using ScottPlot;
using ScottPlot.Palettes;
using ScottPlot.TickGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMove
{
    public class PlotConfiguration
    {
        // methods to configure plots in the UI 
        public double[] defaultXAxisIndex = { 1, 2, 3, 4, 5 };

        public void ApplyConfiguration(ScottPlot.WPF.WpfPlot defaultPlot, double[] dataY, string axesLabel, string eventsFlag = null)
        {       
            
            // add events graphic if not OT dash
            if (eventsFlag != null)
            {
                AddEventsGraphic(defaultPlot, eventsFlag);
            }

            // add gradient effect if not events dash
            if (eventsFlag == null)
            {
                CreateCustomGraphic(defaultPlot, dataY);
            }

            // initialise plot
            InitialisePlot(defaultPlot, dataY);

            // remove user interaction
            LimitInteraction(defaultPlot);

            // general grid, frame, axes colour set
            GeneralFeatureSet(defaultPlot);

            // customise x axis
            CustomiseXAxis(defaultPlot);

            // customise y axis
            CustomiseYAxis(defaultPlot, axesLabel, dataY, eventsFlag);

            // refresh the plot to show the changes
            defaultPlot.Refresh();
        }

        // initialise plot 
        public void InitialisePlot(ScottPlot.WPF.WpfPlot defaultPlot, double[] dataY)
        {
            var myScatter = defaultPlot.Plot.Add.Scatter(defaultXAxisIndex, dataY);

            // customise
            myScatter.Color = ScottPlot.Color.FromHex("#7E334C");
            myScatter.LineWidth = 2;
        }

        // remove user interaction
        public void LimitInteraction(ScottPlot.WPF.WpfPlot defaultPlot)
        {
            // ScottPlot has user interaction enabled by default, this is disabled below (as unrequired)
            ScottPlot.Control.InputBindings customInputBindings = new ScottPlot.Control.InputBindings();
            ScottPlot.Control.Interaction interaction = new ScottPlot.Control.Interaction(defaultPlot)
            {
                Inputs = customInputBindings,
                Actions = ScottPlot.Control.PlotActions.NonInteractive(),
            };
            defaultPlot.Interaction = interaction;
        }

        // add gradient effect
        public void CreateCustomGraphic(ScottPlot.WPF.WpfPlot defaultPlot, double[] dataY)
        {
            double minY = dataY.Min();

            int totalPoints = defaultXAxisIndex.Length + 2;

            Coordinates[] polygonPoints = new Coordinates[totalPoints];

            polygonPoints[0] = new Coordinates(defaultXAxisIndex[0], minY);

            for (int i = 0; i < defaultXAxisIndex.Length; i++)
            {
                polygonPoints[i + 1] = new Coordinates(defaultXAxisIndex[i], dataY[i]);
            }

            polygonPoints[totalPoints - 1] = new Coordinates(defaultXAxisIndex[defaultXAxisIndex.Length - 1], minY);

            var poly = defaultPlot.Plot.Add.Polygon(polygonPoints);
            poly.FillStyle = new FillStyle
            {
                Color = ScottPlot.Color.FromHex("#4B2A46"), //7E334C
                HatchColor = ScottPlot.Color.FromHex("#15141E"),
                Hatch = new Gradient()
                {
                    GradiantType = GradiantType.Linear,
                    AlignmentStart = Alignment.UpperRight,
                    AlignmentEnd = Alignment.LowerLeft,
                }
            };
        }

        // customise x axis
        public void CustomiseXAxis(ScottPlot.WPF.WpfPlot defaultPlot)
        {
            defaultPlot.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericFixedInterval(1);
            defaultPlot.Plot.Axes.Bottom.MinorTickStyle.Color = ScottPlot.Colors.Transparent;
            defaultPlot.Plot.Axes.Bottom.MajorTickStyle.Color = ScottPlot.Color.FromHex("#28273F");

            defaultPlot.Plot.Axes.Bottom.FrameLineStyle.Color = ScottPlot.Color.FromHex("#28273F");
            defaultPlot.Plot.Axes.Bottom.FrameLineStyle.Width = 3;
        }

        // customise y axis
        public void CustomiseYAxis(ScottPlot.WPF.WpfPlot defaultPlot, string plotName, double[] dataY, string eventsFlag = null)
        {
            int interval;

            defaultPlot.Plot.YLabel(GetAxesLabelY(plotName));
            defaultPlot.Plot.Axes.Left.Label.FontSize = 11;

            double minY = dataY.Min();
            double maxY = dataY.Max();
            double rangeY = maxY - minY;

            if (rangeY > 1)
            {
                interval = (int)Math.Ceiling(rangeY / 3);
                defaultPlot.Plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericFixedInterval(interval);
            }
            else
            {
                // create a manual tick generator and add ticks
                ScottPlot.TickGenerators.NumericManual ticks = new();

                // add major ticks with their labels
                ticks.AddMajor(0, "0");
                ticks.AddMajor(0.25, "0.25");
                ticks.AddMajor(0.5, "0.5");
                ticks.AddMajor(1, "1");

                defaultPlot.Plot.Axes.Left.TickGenerator = ticks;
            }


    defaultPlot.Plot.Axes.Left.MajorTickStyle.Color = ScottPlot.Color.FromHex("#28273F");
            defaultPlot.Plot.Axes.Left.MinorTickStyle.Color = ScottPlot.Colors.Transparent;

            defaultPlot.Plot.Axes.Left.FrameLineStyle.Color = ScottPlot.Color.FromHex("#28273F");
            defaultPlot.Plot.Axes.Left.FrameLineStyle.Width = 3;

            if (eventsFlag != null)
            {
                AddEventsCustomXAxis(defaultPlot);
            }
        }

        // general grid, frame, axes colour set
        public void GeneralFeatureSet(ScottPlot.WPF.WpfPlot defaultPlot)
        {
            defaultPlot.Plot.Style.ColorAxes(ScottPlot.Colors.White);

            defaultPlot.Plot.Axes.Top.FrameLineStyle.Color = ScottPlot.Colors.Transparent;
            defaultPlot.Plot.Axes.Right.FrameLineStyle.Color = ScottPlot.Colors.Transparent;

            defaultPlot.Plot.Style.ColorGrids(ScottPlot.Colors.Transparent);
            defaultPlot.Plot.Style.Background(
                figure: ScottPlot.Colors.Transparent,
                data: ScottPlot.Colors.Transparent);
        }

        // code to sort to correct Y axis labels
        public string GetAxesLabelY(string plotName)
        {
            string axesLabelY = "";

            string KASTString = "K A S T ( % )";
            string winrateString = "W I N R A T E ( % )";
            string abilityString = "A V. T O T A L  A B I L I T Y  U S A G E";
            string headshotString = "H E A D S H O T ( % )";

            switch (plotName)
            {
                case "WinRateOT":
                    axesLabelY = winrateString;
                    break;
                case "HeadshotOT":
                    axesLabelY = headshotString;
                    break;
                case "AbilityUsageOT":
                    axesLabelY = abilityString;
                    break;
                case "KASTOT":
                    axesLabelY = KASTString;
                    break;
                case "WinrateEvents":
                    axesLabelY = winrateString;
                    break;
                case "HeadshotEvents":
                    axesLabelY = headshotString;
                    break;
                case "KASTEvents":
                    axesLabelY = KASTString;
                    break;

            }
            return axesLabelY;
        }
      
        public void AddEventsGraphic(ScottPlot.WPF.WpfPlot defaultPlot, string eventsFlag)
        {
           // add events highlight 
           var barCustomisation = defaultPlot.Plot.Add.HorizontalSpan(2.5, 3.5);        
           
           if(eventsFlag == "positiveEvent")
            {
                barCustomisation.FillStyle.Color = ScottPlot.Color.FromHex("#80334C").WithAlpha(.4);
                barCustomisation.LineStyle.Color = ScottPlot.Colors.Transparent;
            }
            else
            {
                barCustomisation.FillStyle.Color = ScottPlot.Color.FromHex("#28273F").WithAlpha(.4);
                barCustomisation.LineStyle.Color = ScottPlot.Colors.Transparent;
            }                    
        }

        public void AddEventsCustomXAxis(ScottPlot.WPF.WpfPlot defaultPlot)
        {
            // set custom ticks for unique x axis
            ScottPlot.TickGenerators.NumericManual ticks = new();

            // add major ticks with their labels
            ticks.AddMajor(1, "-2");
            ticks.AddMajor(2, "-1");
            ticks.AddMajor(3, "0");
            ticks.AddMajor(4, "+1");
            ticks.AddMajor(5, "+2");

            defaultPlot.Plot.Axes.Bottom.TickGenerator = ticks;

            defaultPlot.Plot.XLabel("R O U N D  I N D E X");
            defaultPlot.Plot.Axes.Bottom.Label.FontSize = 11;

        }

    }

}
