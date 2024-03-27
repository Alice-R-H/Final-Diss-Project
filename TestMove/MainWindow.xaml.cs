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
        private RoundResultsRepo roundResultsRepo;

        public MainWindow()
        {
            InitializeComponent();

            // hide/show grids on start-up
            uiConfigurer = new ConfigureUIElements(this);
            uiConfigurer.ConfigureUIOnClick(OTGrid, TimeAnalysisHighlight);

            initialiseVisualisations = new InitialiseVisualisations();
            // Create RoundResultsRepo instance (assuming ModelContext is available for its initialization)
            roundResultsRepo = new RoundResultsRepo(new ModelContext());

            // create InitialiseVisualisations instance
           

            // set dependencies

            initialiseVisualisations.SetRoundResultsRepo(roundResultsRepo);

            // now that all dependencies are set, initialize visualisations
            initialiseVisualisations.SetMainWindow(this);
            initialiseVisualisations.InitialiseAllVisualisations();
        }

        // NAVIGATION BUTTONS
        private void Button_TimeAnalysis(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ConfigureUIOnClick(OTGrid, TimeAnalysisHighlight);
            uiConfigurer.UpdateTitleOnClick(TitleLabel, "Time");
        }

        private void Button_PressureAnalysis(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ConfigureUIOnClick(PressureGrid, PressureAnalysisHighlight);
            uiConfigurer.UpdateTitleOnClick(TitleLabel, "Pressure");
        }

        private void Button_EventsAnalysis(object sender, RoutedEventArgs e)
        {
            uiConfigurer.ConfigureUIOnClick(EventsGrid, EventsAnalysisHighlight);
            uiConfigurer.UpdateTitleOnClick(TitleLabel, "Events");
        }

        // TIME GRID INFO & CLOSE BUTTONS
        private void Button_WinrateInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(WinrateInfoGrid, true);
        }

        private void Button_HeadshotInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(HeadshotInfoGrid, true);
        }

        private void Button_AbilityInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(AbilityInfoGrid, true);
        }

        private void Button_KASTInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(KASTInfoGrid, true);
        }

        private void Button_CloseWinrateInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(WinrateInfoGrid, false);
        }

        private void Button_CloseHeadshotInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(HeadshotInfoGrid, false);
        }

        private void Button_CloseAbilityInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(AbilityInfoGrid, false);
        }

        private void Button_CloseKASTInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(KASTInfoGrid, false);
        }

        private void Button_ClosePMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(PMultiplierInfoGrid, false);
        }

        private void Button_CloseIMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(IMultiplierInfoGrid, false);
        }

        private void Button_IMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(IMultiplierInfoGrid, true);
        }

        private void Button_PMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(PMultiplierInfoGrid, true);
        }

        private void Button_HPIMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(HPIMultiplierInfoGrid, true);
        }

        // PRESSURE GRID INFO & CLOSE BUTTONS

        private void Button_HPPMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(HPPMultiplierInfoGrid, true);
        }

        private void Button_CloseHPPMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(HPPMultiplierInfoGrid, false);
        }

        private void Button_CloseHPIMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(HPIMultiplierInfoGrid, false);
        }

        private void Button_CloseHPIndexInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(HPIndexInfoGrid, false);
        }

        private void Button_HPIndexInfo1(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(HPIndexInfoGrid, true);
        }

        // EVENTS GRID INFO & CLOSE BUTTONS

        private void Button_EIMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(EIMultiplierInfoGrid, true);
        }

        private void Button_EPMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(EPMultiplierInfoGrid, true);
        }

        private void Button_CloseEPMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(EPMultiplierInfoGrid, false);
        }

        private void Button_CloseEIMultiplierInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(EIMultiplierInfoGrid, false);
        }

        private void Button_NegativeEventInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(EventsTrendsInfoGrid, true);
        }

        private void Button_PositiveEventInfo(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(EventsTrendsInfoGrid, true);
        }

        private void Button_CloseEventsTrendsInfoGrid(object sender, RoutedEventArgs e)
        {
            uiConfigurer.OTInfoOnClick(EventsTrendsInfoGrid, false);
        }
    }
}
