using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestMove.Services
{
    public class DatabasePopulation
    {

        // methods to populate and clear the database tables

        // all ranges are +1 to account for exclusion of upper bound

        private ModelContext modelContext;

        public DatabasePopulation()
        {
            modelContext = new ModelContext();
        }

        public async Task SeedDatabaseAsync()
        {
           // populate database on startup

           await SeedRoundResultsAsync();

           await SeedHPRoundResultsAsync();

           await SeedPositiveEventsRoundResultsAsync();

           await SeedNegativeEventsRoundResultsAsync();
        }

        // CLEAR DATABASE

        public async Task ClearDatabaseAsync()
        {
            // clear database on close.
            var allEntriesRoundResults = modelContext.RoundResults.ToList();
            var allEntriesHPRoundResults = modelContext.HPRoundResults.ToList();
            var allEntriesPositiveEventsRoundResults = modelContext.PositiveEventsRoundResults.ToList();
            var allEntriesNegativeEventsRoundResults = modelContext.NegativeEventsRoundResults.ToList();

            modelContext.RoundResults.RemoveRange(allEntriesRoundResults);
            modelContext.HPRoundResults.RemoveRange(allEntriesHPRoundResults);
            modelContext.PositiveEventsRoundResults.RemoveRange(allEntriesPositiveEventsRoundResults);
            modelContext.NegativeEventsRoundResults.RemoveRange(allEntriesNegativeEventsRoundResults);

            await modelContext.SaveChangesAsync();

            // leaving the auto-increment ID as is, as is standard practice and does not impact performance at high values.
        }

        public async Task SeedRoundResultsAsync()
        {
            if (!modelContext.RoundResults.Any())
            {
                var rnd = new Random();
                int roundIdentifier = 1;

                // we create 360 rounds, assuming 24 rounds a game, total of 20 games. 
                for (int i = 0; i < 480; i++)
                {
                    var roundResult = new RoundResults
                    {
                        // the cell is populated randomly between the specified range                        
                        kills = rnd.Next(0, 6),
                        assists = rnd.Next(0, 6),
                        deaths = GetDataBoolBiased(rnd, 0, 2, 0.8),
                        trades = GetDataBoolBiased(rnd, 0, 2, 0.8),
                        abilitiesUsed = rnd.Next(0, 4),
                        HeadshotPercentage = rnd.Next(0, 101),
                        RoundWin = rnd.Next(0, 2) > 0,
                        RoundIdentifier = roundIdentifier,
                    };

                    modelContext.RoundResults.Add(roundResult);

                    roundIdentifier++;
                    if (roundIdentifier > 24)
                    {
                        roundIdentifier = 1;
                    }
                }
                await modelContext.SaveChangesAsync();
            }
        }

        public async Task SeedHPRoundResultsAsync()
        {
            if (!modelContext.HPRoundResults.Any())
            {
                var rnd = new Random();
                int roundIdentifier = 1;

                // we create 360 rounds, assuming 24 rounds a game, total of 20 games. 
                for (int i = 0; i < 480; i++)
                {
                    var roundResultHP = new HPRoundResults
                    {
                        // the cell is populated randomly between the specified range
                        killsHP = rnd.Next(0, 6),
                        assistsHP = rnd.Next(0, 6),
                        deathsHP = rnd.Next(0, 2) > 0,
                        tradesHP = rnd.Next(0, 2) > 0,
                        HeadshotPercentageHP = rnd.Next(0, 101),
                        RoundWinHP = rnd.Next(0, 2) > 0,
                        RoundIdentifierHP = roundIdentifier,
                    };

                    modelContext.HPRoundResults.Add(roundResultHP);

                    roundIdentifier++;
                    if (roundIdentifier > 24)
                    {
                        roundIdentifier = 1;
                    }
                }
                await modelContext.SaveChangesAsync();
            }
        }

        public async Task SeedPositiveEventsRoundResultsAsync()
        {
            if (!modelContext.PositiveEventsRoundResults.Any())
            {
                var rnd = new Random();

                // we create 360 rounds, assuming 24 rounds a game, total of 20 games. 
                for (int i = 0; i < 20; i++)
                {
                    var positiveEventsRoundResults = new PositiveEventsRoundResults
                    {
                        // the cell is populated randomly between the specified range
                        meanKASTPrePE = rnd.Next(0, 101),
                        meanHeadshotPercentagePrePE = rnd.Next(0, 101),
                        roundWinratePrePE = rnd.Next(0, 101),

                        meanKASTPostPE = rnd.Next(0, 101),
                        meanHeadshotPercentagePostPE = rnd.Next(0, 101),
                        roundWinratePostPE = rnd.Next(0, 101),
                    };

                    modelContext.PositiveEventsRoundResults.Add(positiveEventsRoundResults);

                }
                await modelContext.SaveChangesAsync();
            }
        }

        public async Task SeedNegativeEventsRoundResultsAsync()
        {
            if (!modelContext.NegativeEventsRoundResults.Any())
            {
                var rnd = new Random();

                // we create 360 rounds, assuming 24 rounds a game, total of 20 games. 
                for (int i = 0; i < 20; i++)
                {
                    var negativeEventsRoundResults = new NegativeEventsRoundResults
                    {
                        // the cell is populated randomly between the specified range
                        meanKASTPreNE = rnd.Next(0, 101),
                        meanHeadshotPercentagePreNE = rnd.Next(0, 101),
                        roundWinratePreNE = rnd.Next(0, 101),

                        meanKASTPostNE = rnd.Next(0, 101),
                        meanHeadshotPercentagePostNE = rnd.Next(0, 101),
                        roundWinratePostNE = rnd.Next(0, 101),
                    };

                    modelContext.NegativeEventsRoundResults.Add(negativeEventsRoundResults);

                }
                await modelContext.SaveChangesAsync();
            }

          
        }
        public bool GetDataBoolBiased(Random rnd, int min, int max, double biasTowardsMin)
        {
            double rndDouble = rnd.NextDouble();

            // Check if the generated number is less than the bias threshold
            if (rndDouble < biasTowardsMin)
            {
                int minValue = min - 1;
                bool result = minValue == 1;
                return result; // Return the minimum value (more likely)
            }
            else
            {
                int maxValue = max - 1;
                bool result = maxValue == 1;
                return result; // Return the maximum value - 1 (less likely)
            }
        }
    }
}
