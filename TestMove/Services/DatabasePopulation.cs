using Microsoft.EntityFrameworkCore;
using ScottPlot.Plottables;
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

        DatabaseTrendInjector _trendInjector = new DatabaseTrendInjector();

        private ModelContext _modelContext;

        public DatabasePopulation()
        {
            _modelContext = new ModelContext();
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
            var allEntriesRoundResults = _modelContext.RoundResults.ToList();
            var allEntriesHPRoundResults = _modelContext.HPRoundResults.ToList();
            var allEntriesPositiveEventsRoundResults = _modelContext.PositiveEventsRoundResults.ToList();
            var allEntriesNegativeEventsRoundResults = _modelContext.NegativeEventsRoundResults.ToList();

            _modelContext.RoundResults.RemoveRange(allEntriesRoundResults);
            _modelContext.HPRoundResults.RemoveRange(allEntriesHPRoundResults);
            _modelContext.PositiveEventsRoundResults.RemoveRange(allEntriesPositiveEventsRoundResults);
            _modelContext.NegativeEventsRoundResults.RemoveRange(allEntriesNegativeEventsRoundResults);

            await _modelContext.SaveChangesAsync();

            // leaving the auto-increment ID as is, as is standard practice and does not impact performance at high values.
        }

        public async Task SeedRoundResultsAsync()
        {
            if (!_modelContext.RoundResults.Any())
            {
                var rnd = new Random();
                int roundIdentifier = 1;
                int gamesTotal = 1;

                // we create 360 rounds, assuming 24 rounds a game, total of 20 games. 
                for (int i = 0; i < 480; i++)
                {
                    var roundResult = new RoundResults
                    {
                        // the cell is populated randomly between the specified range                        
                        kills = _trendInjector.GetBiasedValue(0, 5, gamesTotal, 3),
                        assists = _trendInjector.GetBiasedValue(0, 5, gamesTotal, 3),
                        deaths = _trendInjector.GenerateBiasedBool(true, gamesTotal, 4),
                        trades = _trendInjector.GenerateBiasedBool(false, gamesTotal, 3),
                        abilitiesUsed = _trendInjector.GetBiasedValue(0, 4, gamesTotal, 3),
                        HeadshotPercentage = _trendInjector.GetBiasedValue(0, 100, gamesTotal, 3),
                        RoundWin = _trendInjector.GenerateBiasedBool(false, gamesTotal, 3),
                        RoundIdentifier = roundIdentifier,
                    };

                    _modelContext.RoundResults.Add(roundResult);

                    roundIdentifier++;
                    if (roundIdentifier > 24)
                    {
                        roundIdentifier = 1;
                        gamesTotal++;
                    }
                }
                await _modelContext.SaveChangesAsync();
            }
        }

        public async Task SeedHPRoundResultsAsync()
        {
            if (!_modelContext.HPRoundResults.Any())
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

                    _modelContext.HPRoundResults.Add(roundResultHP);

                    roundIdentifier++;
                    if (roundIdentifier > 24)
                    {
                        roundIdentifier = 1;
                    }
                }
                await _modelContext.SaveChangesAsync();
            }
        }

        public async Task SeedPositiveEventsRoundResultsAsync()
        {
            if (!_modelContext.PositiveEventsRoundResults.Any())
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

                    _modelContext.PositiveEventsRoundResults.Add(positiveEventsRoundResults);

                }
                await _modelContext.SaveChangesAsync();
            }
        }

        public async Task SeedNegativeEventsRoundResultsAsync()
        {
            if (!_modelContext.NegativeEventsRoundResults.Any())
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

                    _modelContext.NegativeEventsRoundResults.Add(negativeEventsRoundResults);

                }
                await _modelContext.SaveChangesAsync();
            }

          
        }
        
    }
}
