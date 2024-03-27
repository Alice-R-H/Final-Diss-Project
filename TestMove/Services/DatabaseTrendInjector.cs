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

        public int GenerateBiasedValue(int minValue, int maxValue, int gameNumber, double logBias)
        {
            // rate of bias increase can be controlled by the logBias parameter
            double bias = Math.Log(gameNumber, logBias); 
            double range = maxValue - minValue;

            // increasing the bias towards the minimum value
            double biasedRange = range * (1 - bias / (bias + 1));

            // scale and introduce randomness to the bias, then return the biasedValue
            int biasedValue = minValue + (int)(_random.NextDouble() * biasedRange); 

            return biasedValue; 
        }

        public bool GenerateBiasedBool(bool idealReturn, int gameNumber, double logBias)
        {
            double bias = Math.Log(gameNumber, logBias); // rate of bias increasing can be controlled by the logBias parameter
            double probability = bias / (bias + 1); // here we determine a probability factor, based on the bias

            // generate an outcome using the probability determined
            bool boolResult;
            if (idealReturn)
            {
                // if idealReturn is true, we want a higher gameNumber to make true more likely 
                boolResult = _random.NextDouble() >= (1 - probability);
            }
            else
            {
                // if idealReturn is false, we want a higher gameNumber to make false more likely 
                boolResult = _random.NextDouble() < (1 - probability);
            }

            return boolResult;
        }
    }
}
