using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TestMove
{
    internal class InitialiseVisualisations
    {
        PlotConfiguration plotConfig = new PlotConfiguration();
        GaugeConfiguration gaugeConfiguration = new GaugeConfiguration();
        GameStatisticsCalculator gameStatisticsCalculator = new GameStatisticsCalculator();

        private MainWindow _mainWindow;
        private RoundResultsRepo _roundResultsRepo;

        // Allows setting MainWindow after construction
        public void SetMainWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        // Allows setting RoundResultsRepo after construction
        public void SetRoundResultsRepo(RoundResultsRepo roundResultsRepo)
        {
            _roundResultsRepo = roundResultsRepo;
        }

        public List<int> HeadshotPercentages => _roundResultsRepo?.GetAllHeadshotPercentages();       
        public List<int> RoundWin => _roundResultsRepo?.GetAllRoundOutcome();
        public List<int> Kills => _roundResultsRepo?.GetAllKills();
        public List<int> Deaths => _roundResultsRepo?.GetAllDeaths();
        public List<int> Assists => _roundResultsRepo?.GetAllAssists();
        public List<int> Trades => _roundResultsRepo?.GetAllTrades();
        public List<int> AbilitiesUsed => _roundResultsRepo?.GetAllAbilitiesUsed();

        public void InitialiseAllVisualisations()
        {
            InitialisePlots();    
            InitialiseGauges();
        }

        public void InitialisePlots()
        {
            double[] x = { 0.2, 0.5, 0.4 };
            double[] y = { 1, 5, 6 };

            double multiplierX = gameStatisticsCalculator.CalculateMultiplierFromXArray(x);
            double multiplierY = gameStatisticsCalculator.CalculateMultiplierFromXArray(y);

            double averageMultiplier = gameStatisticsCalculator.CalculateAverageMultiplier(multiplierX, multiplierY);

            double beeeee = 1 + 2;

            // static OT data

            // static events data

            // TIME GRID
            double[] dataX = { 1, 2, 3, 4, 5 };

            var headshotAveragePerGame = gameStatisticsCalculator.CalculateAverageXPerGame(HeadshotPercentages);
            int[] headshot5 = headshotAveragePerGame.Take(5).ToArray();
            double[] headshot5Array = Array.ConvertAll(headshot5, x => (double)x);
            plotConfig.ApplyConfiguration(_mainWindow.HeadShotOT, dataX, headshot5Array, "HeadshotOT");

            var winrateAveragePerGame = gameStatisticsCalculator.CalculateXGameResults_FromBooleanList(RoundWin);
            int[] winrate5 = winrateAveragePerGame.Take(5).ToArray();
            double[] winrate5Array = Array.ConvertAll(winrate5, x => (double)x);
            plotConfig.ApplyConfiguration(_mainWindow.WinrateOT, dataX, winrate5Array, "WinRateOT");

            var KASTBooleanList = gameStatisticsCalculator.GenerateKASTBooleanListRoundResults(Kills, Assists, Deaths, Trades);
            var KASTAveragePerGame = gameStatisticsCalculator.CalculateXGameResults_FromBooleanList(KASTBooleanList);
            int[] KAST5 = KASTAveragePerGame.Take(5).ToArray();
            double[] KAST5Array = Array.ConvertAll(KAST5, x => (double)x);
            plotConfig.ApplyConfiguration(_mainWindow.KASTOT, dataX, KAST5Array, "KASTOT");

            var AbilitiesAveragePerGame = gameStatisticsCalculator.CalculateAverageXPerGame(AbilitiesUsed);
            int[] Abilities5 = AbilitiesAveragePerGame.Take(5).ToArray();
            double[] Abilities5Array = Array.ConvertAll(Abilities5, x => (double)x);
            plotConfig.ApplyConfiguration(_mainWindow.AbilityUsageOT, dataX, Abilities5Array, "AbilityUsageOT");

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

    }
}
