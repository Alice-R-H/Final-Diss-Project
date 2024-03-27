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
        PlotConfiguration plotConfig = new PlotConfiguration();
        GaugeConfiguration gaugeConfiguration = new GaugeConfiguration();
        StatsPreparer statsPreparer = new StatsPreparer();

        private MainWindow _mainWindow;

        public void SetMainWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        // Add this method to set the RoundResultsRepo
        public void SetRoundResultsRepo(RoundResultsRepo roundResultsRepo)
        {
            statsPreparer.SetRoundResultsRepo(roundResultsRepo);
        }

        public void InitialiseAllVisualisations()
        {
            InitialiseTimePlots();
            InitialiseEventsPlots();
            InitialiseGauges();
            InitialiseMultipliers();
        }

        public void InitialiseTimePlots()
        {
            double[] gamesPlayed = { 1, 2, 3, 4, 5 };

            double[] headshot5Array = statsPreparer.FetchHeadshotArrayForTime();
            plotConfig.ApplyConfiguration(_mainWindow.HeadShotOT, gamesPlayed, headshot5Array, "HeadshotOT");

            double[] winrate5Array = statsPreparer.FetchWinrateArrayForTime();
            plotConfig.ApplyConfiguration(_mainWindow.WinrateOT, gamesPlayed, winrate5Array, "WinRateOT");

            double[] KAST5Array = statsPreparer.FetchKASTArrayForTime();
            plotConfig.ApplyConfiguration(_mainWindow.KASTOT, gamesPlayed, KAST5Array, "KASTOT");

            double[] Abilities5Array = statsPreparer.FetchAbilitiesArrayForTime();
            plotConfig.ApplyConfiguration(_mainWindow.AbilityUsageOT, gamesPlayed, Abilities5Array, "AbilityUsageOT");
        }

        public void InitialiseEventsPlots()
        {
            // EVENTS GRID
            string positiveEventFlag = "positiveEvent";
            string negativeEventFlag = "negativeEvent";

            double[] dataX5 = { 1, 2, 3, 4, 5 };
            double[] dataY5 = { 56, 53, 52, 48, 46 };
            // pos
            plotConfig.ApplyConfiguration(_mainWindow.WinratePosEvent, dataX5, dataY5, "WinrateEvents", positiveEventFlag);
            plotConfig.ApplyConfiguration(_mainWindow.KASTPosEvent, dataX5, dataY5, "KASTEvents", positiveEventFlag);
            plotConfig.ApplyConfiguration(_mainWindow.HeadshotPosEvent, dataX5, dataY5, "HeadshotEvents", positiveEventFlag);

            // neg
            plotConfig.ApplyConfiguration(_mainWindow.WinrateNegEvent, dataX5, dataY5, "WinrateEvents", negativeEventFlag);
            plotConfig.ApplyConfiguration(_mainWindow.KASTNegEvent, dataX5, dataY5, "KASTEvents", negativeEventFlag);
            plotConfig.ApplyConfiguration(_mainWindow.HeadshotNegEvent, dataX5, dataY5, "HeadshotEvents", negativeEventFlag);

        }

        public void InitialiseGauges()
        {
            // static pressure data
            int highPressureValue = -74;
            int highPressureValue1 = 3;
            int highPressureValue2 = 4;
            int highPressureValue3 = -40;

            gaugeConfiguration.ApplyConfiguration(_mainWindow.WinrateGauge, highPressureValue);
            gaugeConfiguration.ApplyConfiguration(_mainWindow.HeadshotGauge, highPressureValue1);
            gaugeConfiguration.ApplyConfiguration(_mainWindow.KASTGauge, highPressureValue2);
            gaugeConfiguration.ApplyConfiguration(_mainWindow.DeathsGauge, highPressureValue3);
        }

        public void InitialiseMultipliers()
        {
            double performanceMultiplierForTime = statsPreparer.FetchPerformanceMultiplierForTime();
            _mainWindow.PerformanceMultiplierTimeValue.Text = performanceMultiplierForTime.ToString();

            double impactMultiplierForTime = statsPreparer.FetchImpactMultiplierForTime();
            _mainWindow.ImpactMultiplierTimeValue.Text = impactMultiplierForTime.ToString();
        }

    }
}
