using ScottPlot.Panels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Syncfusion.UI.Xaml.Gauges;
using TestMove;
using TestMove.Services;

namespace TestMove
{
    public partial class MainWindow : Window
    {
        private ConfigureUIElements uiConfigurer;
        private InitialiseVisualisations initialiseVisualisations;
        private UIContentStrings uiContentStrings;
        private RoundResultsRepo roundResultsRepo;
        private HPRoundResultsRepo roundHPResultsRepo;
        private NEventsRepo negativeEventsRoundResults;
        private PEventsRepo positiveEventsRoundResults;

        public MainWindow()
        {
          
            InitializeComponent();

            // hide/show grids on start-up
            uiConfigurer = new ConfigureUIElements(this);
            uiConfigurer.ConfigureUIOnClick(OTGrid, TimeAnalysisHighlight, "Time");

            uiContentStrings = new UIContentStrings();           
            initialiseVisualisations = new InitialiseVisualisations();

            // initialise all repo instances using Model Context
            roundResultsRepo = new RoundResultsRepo(new ModelContext());
            roundHPResultsRepo = new HPRoundResultsRepo(new ModelContext());
            negativeEventsRoundResults = new NEventsRepo(new ModelContext());
            positiveEventsRoundResults = new PEventsRepo(new ModelContext());

            // set dependencies
            initialiseVisualisations.SetRoundResultsRepo(roundResultsRepo);
            initialiseVisualisations.SetHPRoundResultsRepo(roundHPResultsRepo);
            initialiseVisualisations.SetNEventsRepo(negativeEventsRoundResults);
            initialiseVisualisations.SetPEventsRepo(positiveEventsRoundResults);

            // now that all dependencies are set, initialize visualisations, threading through MainWindow
            uiContentStrings.SetMainWindow(this);
            initialiseVisualisations.SetMainWindow(this);
            initialiseVisualisations.InitialiseAllVisualisations();
            uiContentStrings.SetAllInformationTileText();
;        }

        // NAVIGATION BUTTONS
        private void Button_TimeAnalysis(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ConfigureUIOnClick(OTGrid, TimeAnalysisHighlight, "Time");
        }

        private void Button_PressureAnalysis(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ConfigureUIOnClick(PressureGrid, PressureAnalysisHighlight, "Pressure");
        }

        private void Button_EventsAnalysis(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ConfigureUIOnClick(EventsGrid, EventsAnalysisHighlight, "Events");
        }

        // TIME GRID INFO & CLOSE BUTTONS
        private void Button_WinrateInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(WinrateInfoGrid, true);
        }

        private void Button_HeadshotInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(HeadshotInfoGrid, true);
        }

        private void Button_AbilityInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(AbilityInfoGrid, true);
        }

        private void Button_KASTInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(KASTInfoGrid, true);
        }

        private void Button_CloseWinrateInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(WinrateInfoGrid, false);
        }

        private void Button_CloseHeadshotInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(HeadshotInfoGrid, false);
        }

        private void Button_CloseAbilityInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(AbilityInfoGrid, false);
        }

        private void Button_CloseKASTInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(KASTInfoGrid, false);
        }

        private void Button_ClosePMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(PMultiplierInfoGrid, false);
        }

        private void Button_CloseIMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(IMultiplierInfoGrid, false);
        }

        private void Button_IMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(IMultiplierInfoGrid, true);
        }

        private void Button_PMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(PMultiplierInfoGrid, true);
        }

        private void Button_HPIMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(HPIMultiplierInfoGrid, true);
        }

        // PRESSURE GRID INFO & CLOSE BUTTONS

        private void Button_HPPMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(HPPMultiplierInfoGrid, true);
        }

        private void Button_CloseHPPMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(HPPMultiplierInfoGrid, false);
        }

        private void Button_CloseHPIMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(HPIMultiplierInfoGrid, false);
        }

        private void Button_CloseHPIndexInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(HPIndexInfoGrid, false);
        }

        private void Button_HPIndexInfo1(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(HPIndexInfoGrid, true);
        }

        // EVENTS GRID INFO & CLOSE BUTTONS

        private void Button_EIMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(EIMultiplierInfoGrid, true);
        }

        private void Button_EPMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(EPMultiplierInfoGrid, true);
        }

        private void Button_CloseEPMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(EPMultiplierInfoGrid, false);
        }

        private void Button_CloseEIMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(EIMultiplierInfoGrid, false);
        }

        private void Button_NegativeEventInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(EventsTrendsInfoGrid, true);
        }

        private void Button_PositiveEventInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(EventsTrendsInfoGrid, true);
        }

        private void Button_CloseEventsTrendsInfoGrid(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ShowInfoOnClick(EventsTrendsInfoGrid, false);
        }
    }
}
