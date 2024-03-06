using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMove.Data
{
    internal class NEventsRepo
    {
        // methods to pull columns from the database and convert to a list for further processing

        private readonly ModelContext modelContext;

        public NEventsRepo(ModelContext context)
        {
            modelContext = context;
        }

        public List<int> GetKASTPre()
        {
            return modelContext.NegativeEventsRoundResults.Select(r => r.meanKASTPreNE).ToList();
        }

        public List<int> GetRoundWinratePre()
        {
            return modelContext.NegativeEventsRoundResults.Select(r => r.roundWinratePreNE).ToList();
        }

        public List<int> GetHeadshotPercentagePre()
        {
            return modelContext.NegativeEventsRoundResults.Select(r => r.meanHeadshotPercentagePreNE).ToList();
        }

        public List<int> GetKASTPost()
        {
            return modelContext.NegativeEventsRoundResults.Select(r => r.meanKASTPostNE).ToList();
        }

        public List<int> GetRoundWinratePost()
        {
            return modelContext.NegativeEventsRoundResults.Select(r => r.roundWinratePostNE).ToList();
        }

        public List<int> GetHeadshotPercentagePost()
        {
            return modelContext.NegativeEventsRoundResults.Select(r => r.meanHeadshotPercentagePostNE).ToList();
        }
    }
}
