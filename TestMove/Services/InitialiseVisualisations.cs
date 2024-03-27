using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using TestMove;

namespace TestMove
{
    internal class InitialiseVisualisations
    {
        // instance statspreparer and config classes
        PlotConfiguration plotConfig = new PlotConfiguration();
        GaugeConfiguration gaugeConfiguration = new GaugeConfiguration();
        StatsPreparer statsPreparer = new StatsPreparer();

        private MainWindow _mainWindow;

        public void SetMainWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        // set all repositories
        // set the RoundResultsRepo
        public void SetRoundResultsRepo(RoundResultsRepo roundResultsRepo)
        {
            statsPreparer.SetRoundResultsRepo(roundResultsRepo);
        }

        // set the HPRoundResultsRepo
        public void SetHPRoundResultsRepo(HPRoundResultsRepo roundHPResultsRepo)
        {
            statsPreparer.SetHPRoundResultsRepo(roundHPResultsRepo);
        }

        // set the NEventsRepo
        public void SetNEventsRepo(NEventsRepo NEventsRepo)
        {
            statsPreparer.SetNEventsRepo(NEventsRepo);
        }

        // set the PEventsRepo
        public void SetPEventsRepo(PEventsRepo pEventsRepo)
        {
            statsPreparer.SetPEventsRepo(pEventsRepo);
        }

        public void InitialiseAllVisualisations()
        {
            // initialise all visualisations
            InitialiseTimePlots();
            InitialiseEventsPlots();
            InitialiseGauges();
            InitialiseMultipliers();
        }

        public void InitialiseTimePlots()
        {
            double[] headshot5Array = statsPreparer.FetchHeadshotArrayForTime();
            plotConfig.ApplyConfiguration(_mainWindow.HeadShotOT, headshot5Array, "HeadshotOT");

            double[] winrate5Array = statsPreparer.FetchWinrateArrayForTime();
            plotConfig.ApplyConfiguration(_mainWindow.WinrateOT, winrate5Array, "WinRateOT");

            double[] KAST5Array = statsPreparer.FetchKASTArrayForTime();
            plotConfig.ApplyConfiguration(_mainWindow.KASTOT, KAST5Array, "KASTOT");

            double[] Abilities5Array = statsPreparer.FetchAbilitiesArrayForTime();
            plotConfig.ApplyConfiguration(_mainWindow.AbilityUsageOT, Abilities5Array, "AbilityUsageOT");
        }

        public void InitialiseEventsPlots()
        {
            // event type flags
            string positiveEventFlag = "positiveEvent";
            string negativeEventFlag = "negativeEvent";

            // event metric flag
            string KASTEvent = "KASTEvents";
            string winrateEvent = "WinrateEvents";
            string headshotEvent = "HeadshotEvents";

            // EVENTS GRID
            // Positive Events Plots
            double[] winratePositiveEventRounds = statsPreparer.FetchHeadshotArrayForPosEvents();
            plotConfig.ApplyConfiguration(_mainWindow.WinratePosEvent, winratePositiveEventRounds, winrateEvent, positiveEventFlag);

            double[] KASTPositiveEventRounds = statsPreparer.FetchKASTArrayForPosEvents();
            plotConfig.ApplyConfiguration(_mainWindow.KASTPosEvent, KASTPositiveEventRounds, KASTEvent, positiveEventFlag);

            double[] headshotPositiveEventRounds = statsPreparer.FetchHeadshotArrayForPosEvents();
            plotConfig.ApplyConfiguration(_mainWindow.HeadshotPosEvent, headshotPositiveEventRounds, headshotEvent, positiveEventFlag);

            // Negative Events Plots
            double[] winrateNegativeEventRounds = statsPreparer.FetchHeadshotArrayForNegEvents();
            plotConfig.ApplyConfiguration(_mainWindow.WinrateNegEvent, winrateNegativeEventRounds, winrateEvent, negativeEventFlag);

            double[] KASTNegativeEventRounds = statsPreparer.FetchHeadshotArrayForNegEvents();
            plotConfig.ApplyConfiguration(_mainWindow.KASTNegEvent, KASTNegativeEventRounds, KASTEvent, negativeEventFlag);

            double[] headshotNegativeEventRounds = statsPreparer.FetchHeadshotArrayForNegEvents();
            plotConfig.ApplyConfiguration(_mainWindow.HeadshotNegEvent, headshotNegativeEventRounds, headshotEvent, negativeEventFlag);

        }

        public void InitialiseGauges()
        {
            // winrate gauge
            int winrateIndex = statsPreparer.FetchWinrateIndexForPressure();
            gaugeConfiguration.ApplyConfiguration(_mainWindow.WinrateGauge, winrateIndex);

            // headshot gauge
            int headshotIndex = statsPreparer.FetchHeadshotIndexForPressure();
            gaugeConfiguration.ApplyConfiguration(_mainWindow.HeadshotGauge, headshotIndex);

            // KAST gauge
            int KASTIndex = statsPreparer.FetchKASTIndexForPressure();
            gaugeConfiguration.ApplyConfiguration(_mainWindow.KASTGauge, KASTIndex);

            // deaths gauge
            int deathsIndex = statsPreparer.FetchDeathsIndexForPressure();
            gaugeConfiguration.ApplyConfiguration(_mainWindow.DeathsGauge, deathsIndex);
        }

        public void InitialiseMultipliers()
        {
            // Time Dashboard
            double performanceMultiplierForTime = statsPreparer.FetchPerformanceMultiplierForTime();            
            _mainWindow.PerformanceMultiplierTimeValue.Text = performanceMultiplierForTime.ToString();

            double impactMultiplierForTime = statsPreparer.FetchImpactMultiplierForTime();
            _mainWindow.ImpactMultiplierTimeValue.Text = impactMultiplierForTime.ToString();

            // Pressure Dashboard
            double performanceMultiplierForPressure = statsPreparer.FetchPerformanceMultiplierForPressure();
            _mainWindow.HPPerformanceMultiplierValue.Text = performanceMultiplierForPressure.ToString();

            double impactMultiplierForPressure = statsPreparer.FetchImpactMultiplierForPressure();
            _mainWindow.HPImpactMultiplierValue.Text = impactMultiplierForPressure.ToString();

            // Events Dashboard
            double performanceMultiplierForPositiveEvents = statsPreparer.FetchPositiveEventPerformanceMultiplier();
            _mainWindow.EPerformanceMultiplierText.Text = performanceMultiplierForPositiveEvents.ToString();

            double performanceMultiplierForNegativeEvents = statsPreparer.FetchNegativeEventPerformanceMultiplier();
            _mainWindow.EImpactMultiplierText.Text = performanceMultiplierForNegativeEvents.ToString();
        }

    }
}
