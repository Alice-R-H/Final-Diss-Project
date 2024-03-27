using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Syncfusion.Windows.Controls.SfNavigator;

namespace TestMove.Services
{
    internal class DatabaseTrendInjector
    {
        // methods which introduce trends in the data, to showcase how these behaviours would present themselves through the applications visualisations. 

        private Random _random = new Random();

        //public bool GenerateBiasedBool(int min, int max, double biasTowardsMin)
        //{
        //    double rndDouble = _random.NextDouble();

        //    // Check if the generated number is less than the bias threshold
        //    if (rndDouble < biasTowardsMin)
        //    {
        //        int minValue = min - 1;
        //        bool result = minValue == 1;
        //        return result; // Return the minimum value (more likely)
        //    }
        //    else
        //    {
        //        int maxValue = max - 1;
        //        bool result = maxValue == 1;
        //        return result; // Return the maximum value - 1 (less likely)
        //    }
        //}

        public int GenerateBiasedNumber2(int minValue, int maxValue, int averageValue, int fluctuation, string trendDirection)
        {
            int lowerBound = 0;
            int upperBound = 0; 

            // determine an upper and lower bound based on suggested fluctuation and direction
            switch (trendDirection)
            {
                case "Positive":
                    lowerBound = averageValue;
                    upperBound = Math.Min(averageValue + fluctuation, maxValue);
                    break;
                case "Negative":
                    lowerBound = Math.Max(averageValue - fluctuation, minValue);
                    upperBound = averageValue;
                    break;
            }

            // produce number within specified bounds
            double result = _random.NextDouble() * (upperBound - lowerBound) + lowerBound;
            int resultAsInt = (int)Math.Round(result);

            if (resultAsInt > maxValue)
            {
                resultAsInt = (int)Math.Floor(result);
            }

            return resultAsInt;
        }

        //public int GenerateBiasedNumber(int minValue, int maxValue, int gameNumber)
        //{
        //    int finalValue;
        //    double defineOdds = 1 / gameNumber;

        //    double rndDouble = _random.NextDouble();

        //    if (rndDouble < defineOdds)
        //    {
        //        int minValue = min - 1;
        //        bool result = minValue == 1;
        //        return result; // Return the minimum value (more likely)
        //    }
        //    else
        //    {
        //        int maxValue = max - 1;
        //        bool result = maxValue == 1;
        //        return result; // Return the maximum value - 1 (less likely)
        //    }


        //    return finalValue;
        //}

        public int GenerateBiasedNumber(int minValue, int maxValue, int gameNumber)
        {
            // Calculate the bias. As gameNumber increases, bias towards minValue increases.
            double bias = Math.Log(gameNumber + 1, 1.2); // You can adjust the base to control the rate of increase of bias.

            // Define the range for the 20% variation of minValue.
            int variationRange = (int)((maxValue - minValue) * 0.5);
            int biasedMax = minValue + variationRange;

            // Generate a random number to decide if we should pick near minValue or anywhere.
            double rndDouble = _random.NextDouble();

            // Adjust the probability of picking a number near minValue.
            // As gameNumber increases, the probability of rndDouble being less than bias increases.
            if (rndDouble < bias / (bias + 1)) // This condition adjusts based on gameNumber; tweak as needed.
            {
                // Pick a number within the biased range (closer to minValue).
                return _random.Next(minValue, biasedMax + 1);
            }
            else
            {
                // Pick any number within the full range.
                return _random.Next(minValue, maxValue + 1);
            }
        }

        public int GetBiasedValue(int minValue, int maxValue, int gameNumber, double logBias)
        {
            Random rnd = new Random();
            double bias = Math.Log(gameNumber + 1, logBias); // Adjust the base of the logarithm to control the rate of bias increase.
            double range = maxValue - minValue;
            double biasedRange = range * (1 - bias / (bias + 1)); // Increasing bias towards minValue
            return minValue + (int)(rnd.NextDouble() * biasedRange);
        }

        public bool GenerateBiasedBool(bool idealReturn, int gameNumber, double logBias)
        {
            //Random rnd = new Random();
            //double bias = Math.Log(gameNumber + 1, logBias); // Adjust the base of the logarithm to control the rate of bias increase.
            //double probability = bias / (bias + 1); // Probability of returning false, increases with gameNumber

            //if (idealReturn == true)
            //{

            //}

            //return rnd.NextDouble() >= probability;

            Random rnd = new Random();
            double bias = Math.Log(gameNumber + 1, logBias); // Adjust the base of the logarithm to control the rate of bias increase.
            double probability = bias / (bias + 1); // Probability factor, based on gameNumber and logBias

            // Determine the outcome probability
            bool outcome;
            if (idealReturn)
            {
                // If idealReturn is true, adjust so higher gameNumber makes true more likely
                outcome = rnd.NextDouble() >= (1 - probability);
            }
            else
            {
                // If idealReturn is false, adjust so higher gameNumber makes false more likely
                outcome = rnd.NextDouble() < (1 - probability);
            }

            return outcome;
        }
    }
}
