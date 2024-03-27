using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestMove
{
    internal class StatsPreparer
    {
        GameStatisticsCalculator gameStatisticsCalculator = new GameStatisticsCalculator();

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

        // fetch all data
        // normal rounds
        public List<int> HeadshotPercentages => _roundResultsRepo?.GetAllHeadshotPercentages();
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

        // positive events
        public List<int> meanKASTPrePE => _pEventsRepo?.GetKASTPre();
        public List<int> roundWinratePrePE => _pEventsRepo?.GetRoundWinratePre();
        public List<int> meanHeadshotPercentagePrePE => _pEventsRepo?.GetHeadshotPercentagePre();
        public List<int> meanKASTPostPE => _pEventsRepo?.GetKASTPost();
        public List<int> roundWinratePostPE => _pEventsRepo?.GetRoundWinratePost();
        public List<int> meanHeadshotPercentagePostPE => _pEventsRepo?.GetHeadshotPercentagePost();

        // negative events
        public List<int> meanKASTPreNE => _nEventsRepo?.GetKASTPre();
        public List<int> roundWinratePreNE => _nEventsRepo?.GetRoundWinratePre();
        public List<int> meanHeadshotPercentagePreNE => _nEventsRepo?.GetHeadshotPercentagePre();
        public List<int> meanKASTPostNE => _nEventsRepo?.GetKASTPost();
        public List<int> roundWinratePostNE => _nEventsRepo?.GetRoundWinratePost();
        public List<int> meanHeadshotPercentagePostNE => _nEventsRepo?.GetHeadshotPercentagePost();
      
        // TIME DASHBOARD
        // plots
        public double[] FetchHeadshotArrayForTime()
        {
            var headshotAveragePerGame = gameStatisticsCalculator.CalculateAveragePerGame(HeadshotPercentages);
            int[] headshotAveragePerGameInt = headshotAveragePerGame.Select(d => (int)d).ToArray();
            int[] headshot5 = headshotAveragePerGameInt.Take(5).ToArray();
            double[] headshot5Array = Array.ConvertAll(headshot5, x => (double)x);
            return headshot5Array;
        }

        public double[] FetchWinrateArrayForTime()
        {
            var winrateAveragePerGame = gameStatisticsCalculator.CalculateAveragePerGameResults_FromBooleanList(RoundWin);
            int[] winrate5 = winrateAveragePerGame.Take(5).ToArray();
            double[] winrate5Array = Array.ConvertAll(winrate5, x => (double)x);
            return winrate5Array;
        }

        public double[] FetchKASTArrayForTime()
        {
            var KASTBooleanList = gameStatisticsCalculator.GenerateKASTBooleanListRoundResults(Kills, Assists, Deaths, Trades);
            var KASTAveragePerGame = gameStatisticsCalculator.CalculateAveragePerGameResults_FromBooleanList(KASTBooleanList);
            int[] KAST5 = KASTAveragePerGame.Take(5).ToArray();
            double[] KAST5Array = Array.ConvertAll(KAST5, x => (double)x);
            return KAST5Array;
        }

        public double[] FetchAbilitiesArrayForTime()
        {
            var abilitiesUsedAveragePerGame = gameStatisticsCalculator.CalculateAveragePerGameResults_FromBooleanList(AbilitiesUsed);           
            int[] abilities5 = abilitiesUsedAveragePerGame.Take(5).ToArray();
            double[] abilities5Array = Array.ConvertAll(abilities5, x => (double)x);
            return abilities5Array;
        }

        // multipliers
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

        // PRESSURE DASHBOARD
        // all index values assume that players do not do over 100% better for simplicity, this would be adjusted in the future.
        // gauges
        public int FetchWinrateIndexForPressure()
        {
            double winrateIndex;
            int totalWinrateRounds = RoundWin.Count;
            int totalHPWinrateRounds = HPRoundWin.Count;

            double winrateAverageAllNormalRounds = gameStatisticsCalculator.CalculatePercentageAcrossRounds_FromBooleanList(RoundWin);

            double winrateAverageAllHPRounds = gameStatisticsCalculator.CalculatePercentageAcrossRounds_FromBooleanList(HPRoundWin);

            double winratePercentageDifference = (winrateAverageAllHPRounds / winrateAverageAllNormalRounds) * 100;
            
            winrateIndex = winratePercentageDifference - 100;

            int winrateIndexInt = (int)Math.Round(winrateIndex);

            return winrateIndexInt;
        }

        public int FetchKASTIndexForPressure()
        {
            double KASTIndex;
          
            List<int> KASTAllNormalRounds = gameStatisticsCalculator.GenerateKASTBooleanListRoundResults(Kills, Assists, Deaths, Trades);
            double KASTAverageAllNormalRounds = gameStatisticsCalculator.CalculatePercentageAcrossRounds_FromBooleanList(KASTAllNormalRounds);

            List<int> KASTAllHPRounds = gameStatisticsCalculator.GenerateKASTBooleanListRoundResults(HPKills, HPAssists, HPDeaths, HPTrades);
            double KASTAverageAllHPRounds = gameStatisticsCalculator.CalculatePercentageAcrossRounds_FromBooleanList(KASTAllHPRounds);

            double KASTPercentageDifference = (KASTAverageAllHPRounds / KASTAverageAllNormalRounds) * 100;

            KASTIndex = KASTPercentageDifference - 100;

            int KASTIndexInt = (int)Math.Round(KASTIndex);

            return KASTIndexInt;
        }

        public int FetchHeadshotIndexForPressure()
        {
            double headshotIndex;
          
            double headshotAverageAllRounds = gameStatisticsCalculator.CalculateAverageOfAllRounds(HeadshotPercentages);

            double headshotAverageAllHPRounds = gameStatisticsCalculator.CalculateAverageOfAllRounds(HPHeadshotPercentages);

            double headshotPercentageDifference = (headshotAverageAllHPRounds / headshotAverageAllRounds) * 100;

            headshotIndex = headshotPercentageDifference - 100;

            int headshotIndexInt = (int)Math.Round(headshotIndex);

            return headshotIndexInt;
        }

        public int FetchDeathsIndexForPressure()
        {
            double deathsIndex;

            double deathsAverageAllNormalRounds = gameStatisticsCalculator.CalculatePercentageAcrossRounds_FromBooleanList(Deaths);

            double deathsAverageAllHPRounds = gameStatisticsCalculator.CalculatePercentageAcrossRounds_FromBooleanList(HPDeaths);

            double headsPercentageDifference = (deathsAverageAllHPRounds / deathsAverageAllNormalRounds) * 100;

            deathsIndex = headsPercentageDifference - 100;

            int deathsIndexInt = (int)Math.Round(deathsIndex);

            return deathsIndexInt;
        }

        // multipliers
        public double FetchPerformanceMultiplierForPressure()
        {
            double performanceMultiplierForPressure;

            int headshotIndex = FetchHeadshotIndexForPressure();
            int KASTIndex = FetchKASTIndexForPressure();

            headshotIndex = headshotIndex + 100;
            KASTIndex = KASTIndex + 100;

            double performancePercentageMultiplierForPressure = (headshotIndex + KASTIndex) / 2;

            performanceMultiplierForPressure = performancePercentageMultiplierForPressure / 100;

            performanceMultiplierForPressure = Math.Round(performanceMultiplierForPressure, 1);

            return performanceMultiplierForPressure;
        }

        public double FetchImpactMultiplierForPressure()
        {
            double impactMultiplierForPressure;

            int KASTIndex = FetchKASTIndexForPressure();

            KASTIndex = KASTIndex + 100;

            double KASTIndexDouble = (double)KASTIndex;

            impactMultiplierForPressure = KASTIndexDouble / 100;

            impactMultiplierForPressure = Math.Round(impactMultiplierForPressure, 1);

            return impactMultiplierForPressure;
        }

        // EVENTS DASHBOARD
        // events plots
        // Positive Events
        public double[] FetchHeadshotArrayForPosEvents()
        {
            double[] headshotPositiveEventsArray = FetchEventsArray(meanHeadshotPercentagePrePE, meanHeadshotPercentagePostPE);

            return headshotPositiveEventsArray;
        }

        public double[] FetchWinrateArrayForPosEvents()
        {
            double[] winratePositiveEventsArray = FetchEventsArray(roundWinratePrePE, roundWinratePostPE);

            return winratePositiveEventsArray;
        }

        public double[] FetchKASTArrayForPosEvents()
        {
            double[] KASTPositiveEventsArray = FetchEventsArray(meanKASTPrePE, meanKASTPostPE);

            return KASTPositiveEventsArray;
        }

        // Negative Events
        public double[] FetchHeadshotArrayForNegEvents()
        {
            double[] headshotNegativeEventsArray = FetchEventsArray(meanHeadshotPercentagePreNE, meanHeadshotPercentagePostNE);

            return headshotNegativeEventsArray;
        }

        public double[] FetchWinrateArrayForNegEvents()
        {
            double[] winrateNegativeEventsArray = FetchEventsArray(roundWinratePreNE, roundWinratePostNE);

            return winrateNegativeEventsArray;
        }

        public double[] FetchKASTArrayForNegEvents()
        {
            double[] KASTNegativeEventsArray = FetchEventsArray(meanKASTPreNE, meanKASTPostNE);

            return KASTNegativeEventsArray;
        }

        // Performance Multiplier Events
        public double FetchPositiveEventPerformanceMultiplier()
        {
            double averageMultiplier = FetchAverageEventsMultiplier(meanKASTPrePE, meanHeadshotPercentagePrePE, meanKASTPostPE, meanHeadshotPercentagePostPE);
            return averageMultiplier;
        }

        public double FetchNegativeEventPerformanceMultiplier()
        {
            double averageMultiplier = FetchAverageEventsMultiplier(meanKASTPreNE, meanHeadshotPercentagePreNE, meanKASTPostNE, meanHeadshotPercentagePostNE);
            return averageMultiplier;
        }

        // misc methods to prepare events arrays
        public double[] FetchEventsArray(List<int> PreEventList, List<int> PostEventList)
        {
            double PreEventAverage = gameStatisticsCalculator.CalculateAverageOfAllRounds(PreEventList);
            double PostEventAverage = gameStatisticsCalculator.CalculateAverageOfAllRounds(PostEventList);

            var rnd = new Random();
            
            double roundIndex1 = PreEventAverage + (PreEventAverage * ((rnd.Next(-2, 3) * 0.1)));
            double roundIndex2 = PreEventAverage + (PreEventAverage * ((rnd.Next(-2, 3) * 0.1)));

            double roundIndexEvent = (PostEventAverage + PreEventAverage) / 2;

            double roundIndex4 = PostEventAverage + (PostEventAverage * ((rnd.Next(-2, 3) * 0.1)));
            double roundIndex5 = PostEventAverage + (PostEventAverage * ((rnd.Next(-2, 3) * 0.1)));

            double[] eventsArray = [roundIndex1, roundIndex2, roundIndexEvent, roundIndex4, roundIndex5];

            for (int i = 0; i < eventsArray.Length; i++)
            {
                eventsArray[i] = Math.Round(eventsArray[i], 0);
            }

            return eventsArray;
        }
   
        // misc method to prepare events multipliers
        public double FetchAverageEventsMultiplier(List<int> meanKASTPre, List<int> meanHeadshotPre, List<int> meanKASTPost, List<int> meanHeadshotPost)
        {
            double PreHeadshotEventAverage = gameStatisticsCalculator.CalculateAverageOfAllRounds(meanKASTPre);
            double PostHeadshotEventAverage = gameStatisticsCalculator.CalculateAverageOfAllRounds(meanKASTPost);

            double PreKASTEventAverage = gameStatisticsCalculator.CalculateAverageOfAllRounds(meanHeadshotPre);
            double PostKASTEventAverage = gameStatisticsCalculator.CalculateAverageOfAllRounds(meanHeadshotPost);

            double KASTMultiplier = PostKASTEventAverage / PreKASTEventAverage;
            double headshotMultiplier = PostHeadshotEventAverage / PreHeadshotEventAverage;

            double averageMultiplier = (KASTMultiplier + headshotMultiplier) / 2;
            averageMultiplier = Math.Round(averageMultiplier, 1);

            return averageMultiplier;
        }
    }
}
