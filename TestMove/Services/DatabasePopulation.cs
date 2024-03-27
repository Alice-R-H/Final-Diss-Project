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

        // set model context
        public DatabasePopulation()
        {
            _modelContext = new ModelContext();
        }

        public async Task SeedDatabaseAsync()
        {
           // populate each database table on startup

           await SeedRoundResultsAsync();

           await SeedHPRoundResultsAsync();

           await SeedPosEventsRoundResultsAsync();

           await SeedNegEventsRoundResultsAsync();
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

        // round results database
        public async Task SeedRoundResultsAsync()
        {
            if (!_modelContext.RoundResults.Any())
            {
                int roundIdentifier = 1;
                int gamesTotal = 1;

                // we create 360 rounds, assuming 24 rounds a game, total of 20 games. 
                for (int i = 0; i < 480; i++)
                {
                    var roundResult = new RoundResults
                    {
                        // the cell is populated randomly between the specified range                  
                        kills = _trendInjector.GenerateBiasedValue(0, 6, gamesTotal, 2),
                        assists = _trendInjector.GenerateBiasedValue(0, 6, gamesTotal, 2),
                        deaths = _trendInjector.GenerateBiasedBool(true, gamesTotal, 2),
                        trades = _trendInjector.GenerateBiasedBool(false, gamesTotal, 2),
                        abilitiesUsed = _trendInjector.GenerateBiasedValue(0, 5, gamesTotal, 4),
                        HeadshotPercentage = _trendInjector.GenerateBiasedValue(0, 101, gamesTotal, 5),
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

        // high-pressure round results database
        public async Task SeedHPRoundResultsAsync()
        {
            if (!_modelContext.HPRoundResults.Any())
            {
                int roundIdentifier = 1;
                int gamesTotal = 1;

                // we create 360 rounds, assuming 24 rounds a game, total of 20 games. 
                for (int i = 0; i < 480; i++)
                {
                    var roundResultHP = new HPRoundResults
                    {
                        // the cell is populated randomly between the specified range                        
                        killsHP = _trendInjector.GenerateBiasedValue(0, 3, gamesTotal, 2),
                        assistsHP = _trendInjector.GenerateBiasedValue(0, 3, gamesTotal, 2),
                        deathsHP = _trendInjector.GenerateBiasedBool(true, gamesTotal, 2),
                        tradesHP = _trendInjector.GenerateBiasedBool(false, gamesTotal, 2),
                        HeadshotPercentageHP = _trendInjector.GenerateBiasedValue(0, 70, gamesTotal, 2),
                        RoundWinHP = _trendInjector.GenerateBiasedBool(false, gamesTotal, 2),
                        RoundIdentifierHP = roundIdentifier,
                    };

                    _modelContext.HPRoundResults.Add(roundResultHP);

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

        // positive events round results database
        public async Task SeedPosEventsRoundResultsAsync()
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
                        meanKASTPrePE = rnd.Next(60, 90),
                        meanHeadshotPercentagePrePE = rnd.Next(10, 40),
                        roundWinratePrePE = rnd.Next(40, 60),

                        meanKASTPostPE = rnd.Next(70, 80),
                        meanHeadshotPercentagePostPE = rnd.Next(35, 46),
                        roundWinratePostPE = rnd.Next(50, 70),
                    };

                    _modelContext.PositiveEventsRoundResults.Add(positiveEventsRoundResults);

                }
                await _modelContext.SaveChangesAsync();
            }
        }

        // negative events round results database
        public async Task SeedNegEventsRoundResultsAsync()
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
                        meanKASTPreNE = rnd.Next(70, 85),
                        meanHeadshotPercentagePreNE = rnd.Next(30, 45),
                        roundWinratePreNE = rnd.Next(50, 56),

                        meanKASTPostNE = rnd.Next(40, 60),
                        meanHeadshotPercentagePostNE = rnd.Next(10, 25),
                        roundWinratePostNE = rnd.Next(35, 50),
                    };

                    _modelContext.NegativeEventsRoundResults.Add(negativeEventsRoundResults);

                }
                await _modelContext.SaveChangesAsync();
            }      
        }


        // default random seed
        //// the cell is populated randomly between the specified range
        //killsHP = rnd.Next(0, 6),
        //                assistsHP = rnd.Next(0, 6),
        //                deathsHP = rnd.Next(0, 2) > 0,
        //                tradesHP = rnd.Next(0, 2) > 0,
        //                HeadshotPercentageHP = rnd.Next(0, 101),
        //                RoundWinHP = rnd.Next(0, 2) > 0,
        //                RoundIdentifierHP = roundIdentifier,
    }
}
