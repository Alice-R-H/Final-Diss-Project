using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMove
{
    internal class RoundResultsRepo
    {
        // methods to pull columns from the database and convert to a list for further processing

        private readonly ModelContext modelContext;

        public RoundResultsRepo(ModelContext context)
        {
            modelContext = context;
        }

        public List<int> GetAllKills()
        {
            return modelContext.RoundResults.Select(r => r.kills).ToList();
        }

        public List<int> GetAllAssists()
        {
            return modelContext.RoundResults.Select(r => r.assists).ToList();
        }

        public List<int> GetAllDeaths()
        {
            return modelContext.RoundResults.Select(r => r.deaths ? 1 : 0).ToList(); // if deaths true, set value 1, else 0
        }

        public List<int> GetAllTrades()
        {
            return modelContext.RoundResults.Select(r => r.trades ? 1 : 0).ToList(); // if deaths true, set value 1, else 0
        }

        public List<int> GetAllAbilitiesUsed()
        {
            return modelContext.RoundResults.Select(r => r.abilitiesUsed).ToList(); 
        }

        public List<int> GetAllHeadshotPercentages()
        {
            return modelContext.RoundResults.Select(r => r.HeadshotPercentage).ToList();
        }

        public List<int> GetAllRoundOutcome()
        {
            return modelContext.RoundResults.Select(r => r.RoundWin ? 1 : 0).ToList(); // if deaths true, set value 1, else 0
        }

        public List<int> GetAllRoundIdentifier()
        {
            return modelContext.RoundResults.Select(r => r.RoundIdentifier).ToList();
        }
    }
}
