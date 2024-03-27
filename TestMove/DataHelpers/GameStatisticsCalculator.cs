using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestMove
{
    internal class GameStatisticsCalculator
    {
        // generate KAST rounds list
        public List<int> GenerateKASTBooleanListRoundResults(List<int> Kills, List<int> Assists, List<int> Deaths, List<int> Trades)
        {        
            List<int> resultsAsBooleanList = new List<int>();

            // creating a list of KAST results
            // sort through each index, if any value is > 0, kast criteria is met
            for (int i = 0; i < Kills.Count; i++)
            {
                    // check values of each list at index i are > 0, and there was no deaths that rounds
                    if (Kills[i] > 0 || Assists[i] > 0 || Deaths[i] == 0 || Trades[i] > 0)
                    {
                        resultsAsBooleanList.Add(1); // kast is true
                    }
                    else
                    {
                        resultsAsBooleanList.Add(0); // kast is false
                    }                
            } 
            
            return resultsAsBooleanList;
        }

        // calculate average 
        public List<int> CalculateAveragePerGameResults_FromBooleanList(List<int> resultsAsBooleanList, int totalRounds = 24)
        {
            List<int> resultsPerGame = new List<int>();

            // split the list into rounds per game, and then calculate the average KAST per game
            for (int i = 0; i < resultsAsBooleanList.Count; i += totalRounds)
            {
                // get next 24 items from the current position
                List<int> resultsKASTGame = resultsAsBooleanList.GetRange(i, Math.Min(totalRounds, resultsAsBooleanList.Count - i));

                // calculate the percentage KAST of the chunk
                int percentageKASTThisGame = CalculatePercentageAcrossRounds_FromBooleanList(resultsKASTGame);

                resultsPerGame.Add(percentageKASTThisGame);
            }

            return resultsPerGame;
        }

        public int CalculatePercentageAcrossRounds_FromBooleanList(List<int> XRoundResults)
        {
            double percentageXPerGame;
            int roundsXTrue = 0;

            // sort through each index, if any value is > 0, kast is true
            for (int i = 0; i < XRoundResults.Count; i++)
            {
                // check values of each list at index i are > 0
                if (XRoundResults [i] > 0)
                {
                    roundsXTrue++; // kast is true
                }
            }

            // calculate percentage that X is true
            percentageXPerGame = ((double)roundsXTrue / XRoundResults.Count) * 100;
            int percentageXPerGameRounded = (int)Math.Round(percentageXPerGame);

            return percentageXPerGameRounded;
        }

        public List<double> CalculateAveragePerGame(List<int> X)
        {
            // this method is called for any non-boolean list
            // using LINQ to split the data into "games", then find the average value per game

            int roundsInAGame = 24;

            var averages = X
                .Select((value, index) => new { Index = index, Value = value })
                .GroupBy(x => x.Index / roundsInAGame)
                .Select(group => Math.Round(group.Average(item => item.Value)))
                .ToList();

            return averages;
        }
        
        public double CalculateAverageOfAllRounds(List<int> roundMetric)
        {
            double averageMetric;

            double totalSumOfMetrics = 0;

            foreach (var metric in roundMetric)
            {
                totalSumOfMetrics += metric;
            }

            averageMetric = totalSumOfMetrics / roundMetric.Count;
            averageMetric = Math.Round(averageMetric, 2);

            return averageMetric;
        }

        // multiplier methods
        public double CalculateMultiplierFromXArray(double[] XArray)
        {
            double multiplier;

            double totalSumOfMultipliers = 0;

            for (int i = 1; i < XArray.Length; i++)
            {
                multiplier = XArray[i] / XArray[i - 1];
                totalSumOfMultipliers += multiplier;
            }

            multiplier = totalSumOfMultipliers / (XArray.Length - 1);
            multiplier = Math.Round(multiplier, 5);

            return multiplier;
        }

        public double CalculateAverageMultiplier(params double[] multipliers)
        {
            double averageMultiplier;

            double totalSumOfMultipliers = 0;

            foreach (var multiplier in multipliers)
            {
                totalSumOfMultipliers += multiplier;
            }

            averageMultiplier = totalSumOfMultipliers / multipliers.Length;
            averageMultiplier = Math.Round(averageMultiplier, 5);

            return averageMultiplier;
        }
    }
}
