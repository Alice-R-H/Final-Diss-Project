using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMove
{
    internal class HPRoundResultsRepo
    {
        // methods to pull columns from the database and convert to a list for further processing

        private readonly ModelContext modelContext;

        public HPRoundResultsRepo(ModelContext context)
        {
            modelContext = context;
        }

        public List<int> GetAllKills()
        {
            return modelContext.HPRoundResults.Select(r => r.killsHP).ToList();
        }

        public List<int> GetAllAssists()
        {
            return modelContext.HPRoundResults.Select(r => r.assistsHP).ToList();
        }

        public List<int> GetAllDeaths()
        {
            return modelContext.HPRoundResults.Select(r => r.deathsHP ? 1 : 0).ToList(); // if deaths true, set value 1, else 0
        }

        public List<int> GetAllTrades()
        {
            return modelContext.HPRoundResults.Select(r => r.tradesHP ? 1 : 0).ToList(); // if deaths true, set value 1, else 0
        }

        public List<int> GetAllHeadshotPercentages()
        {
            return modelContext.HPRoundResults.Select(r => r.HeadshotPercentageHP).ToList();

        }

        public List<int> GetAllRoundOutcome()
        {
            return modelContext.HPRoundResults.Select(r => r.RoundWinHP ? 1 : 0).ToList(); // if deaths true, set value 1, else 0
        }

        public List<int> GetAllRoundIdentifier()
        {
            return modelContext.HPRoundResults.Select(r => r.RoundIdentifierHP).ToList();
        }
    }
}
