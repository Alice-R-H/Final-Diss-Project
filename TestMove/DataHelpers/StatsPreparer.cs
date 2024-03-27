using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMove
{
    internal class StatsPreparer
    {
        private RoundResultsRepo _roundResultsRepo;

        // Allows setting RoundResultsRepo after construction
        public void SetRoundResultsRepo(RoundResultsRepo roundResultsRepo)
        {
            _roundResultsRepo = roundResultsRepo;
        }

        private HPRoundResultsRepo _roundResultsHPRepo;

        public void SetHPRoundResultsRepo(HPRoundResultsRepo roundHPResultsRepo)
        {
            _roundResultsHPRepo = roundHPResultsRepo;
        }

        private NEventsRepo _nEventsRepo;

        public void SetNEventsRepo(NEventsRepo nEventsRepo)
        {
            _nEventsRepo = nEventsRepo;
        }

        private PEventsRepo _pEventsRepo;

        public void SetPEventsRepo(PEventsRepo pEventsRepo)
        {
            _pEventsRepo = pEventsRepo;
        }

        // public List<int> HeadshotPercentages => _roundResultsRepo.GetAllHeadshotPercentages();

        GameStatisticsCalculator gameStatisticsCalculator = new GameStatisticsCalculator();

        //// fetch all data
        //// normal rounds
        public List<int> HeadshotPercentages => _roundResultsRepo.GetAllHeadshotPercentages();
        public List<int> RoundWin => _roundResultsRepo?.GetAllRoundOutcome();
        public List<int> Kills => _roundResultsRepo?.GetAllKills();
        public List<int> Deaths => _roundResultsRepo?.GetAllDeaths();
        public List<int> Assists => _roundResultsRepo?.GetAllAssists();
        public List<int> Trades => _roundResultsRepo?.GetAllTrades();
        public List<int> AbilitiesUsed => _roundResultsRepo?.GetAllAbilitiesUsed();

        // high pressure rounds
        public List<int> HPHeadshotPercentages => _roundResultsHPRepo?.GetAllHeadshotPercentages();
        public List<int> HPRoundWin => _roundResultsHPRepo?.GetAllRoundOutcome();
        public List<int> HPKills => _roundResultsHPRepo?.GetAllKills();
        public List<int> HPDeaths => _roundResultsHPRepo?.GetAllDeaths();
        public List<int> HPAssists => _roundResultsHPRepo?.GetAllAssists();
        public List<int> HPTrades => _roundResultsHPRepo?.GetAllTrades();

        // negative events
        public List<int> meanKASTPreNE => _nEventsRepo?.GetKASTPre();
        public List<int> roundWinratePreNE => _nEventsRepo?.GetRoundWinratePre();
        public List<int> meanHeadshotPercentagePreNE => _nEventsRepo?.GetHeadshotPercentagePre();
        public List<int> meanKASTPostNE => _nEventsRepo?.GetKASTPost();
        public List<int> roundWinratePostNE => _nEventsRepo?.GetRoundWinratePost();
        public List<int> meanHeadshotPercentagePostNE => _nEventsRepo?.GetHeadshotPercentagePost();

        // positive events
        public List<int> meanKASTPrePE => _pEventsRepo?.GetKASTPre();
        public List<int> roundWinratePrePE => _pEventsRepo?.GetRoundWinratePre();
        public List<int> meanHeadshotPercentagePrePE => _pEventsRepo?.GetHeadshotPercentagePre();
        public List<int> meanKASTPostPE => _pEventsRepo?.GetKASTPost();
        public List<int> roundWinratePostPE => _pEventsRepo?.GetRoundWinratePost();
        public List<int> meanHeadshotPercentagePostPE => _pEventsRepo?.GetHeadshotPercentagePost();

        public double[] FetchHeadshotArrayForTime()
        {
            var headshotAveragePerGame = gameStatisticsCalculator.CalculateAverageXPerGame(HeadshotPercentages);
            int[] headshot5 = headshotAveragePerGame.Take(5).ToArray();
            double[] headshot5Array = Array.ConvertAll(headshot5, x => (double)x);
            return headshot5Array;
        }

        public double[] FetchWinrateArrayForTime()
        {
            var winrateAveragePerGame = gameStatisticsCalculator.CalculateXGameResults_FromBooleanList(RoundWin);
            int[] winrate5 = winrateAveragePerGame.Take(5).ToArray();
            double[] winrate5Array = Array.ConvertAll(winrate5, x => (double)x);
            return winrate5Array;
        }

        public double[] FetchKASTArrayForTime()
        {
            var KASTBooleanList = gameStatisticsCalculator.GenerateKASTBooleanListRoundResults(Kills, Assists, Deaths, Trades);
            var KASTAveragePerGame = gameStatisticsCalculator.CalculateXGameResults_FromBooleanList(KASTBooleanList);
            int[] KAST5 = KASTAveragePerGame.Take(5).ToArray();
            double[] KAST5Array = Array.ConvertAll(KAST5, x => (double)x);
            return KAST5Array;
        }

        public double[] FetchAbilitiesArrayForTime()
        {
            var AbilitiesAveragePerGame = gameStatisticsCalculator.CalculateAverageAbilitiesPerGame(AbilitiesUsed);
            double[] Abilities5 = AbilitiesAveragePerGame.Take(5).ToArray();
            double[] Abilities5Array = Array.ConvertAll(Abilities5, x => (double)x);
            return Abilities5Array;
        }

        public double FetchPerformanceMultiplierForTime()
        {
            double[] winrateArray = FetchHeadshotArrayForTime();
            double winrateMultiplier = gameStatisticsCalculator.CalculateMultiplierFromXArray(winrateArray);

            double[] KASTArray = FetchKASTArrayForTime();
            double KASTMultiplier = gameStatisticsCalculator.CalculateMultiplierFromXArray(KASTArray);

            double[] multiplierArray = [winrateMultiplier, KASTMultiplier];

            double averageMultiplier = gameStatisticsCalculator.CalculateAverageMultiplier(multiplierArray);
            
            return averageMultiplier;
        }

        public double FetchImpactMultiplierForTime()
        {
            double[] abilitiesArray = FetchAbilitiesArrayForTime();
            double abilitiesMultiplier = gameStatisticsCalculator.CalculateMultiplierFromXArray(abilitiesArray);

            double[] KASTArray = FetchKASTArrayForTime();
            double KASTMultiplier = gameStatisticsCalculator.CalculateMultiplierFromXArray(KASTArray);

            double[] multiplierArray = [abilitiesMultiplier, KASTMultiplier];

            double averageMultiplier = gameStatisticsCalculator.CalculateAverageMultiplier(multiplierArray);

            return averageMultiplier;
        }



    }
}
