using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMove.Data
{
    internal class PEventsRepo
    {
        // methods to pull columns from the database and convert to a list for further processing

        private readonly ModelContext modelContext;

        public PEventsRepo(ModelContext context)
        {
            modelContext = context;
        }

        public List<int> GetKASTPre()
        {
            return modelContext.PositiveEventsRoundResults.Select(r => r.meanKASTPrePE).ToList();
        }

        public List<int> GetRoundWinratePre()
        {
            return modelContext.PositiveEventsRoundResults.Select(r => r.roundWinratePrePE).ToList();
        }

        public List<int> GetHeadshotPercentagePre()
        {
            return modelContext.PositiveEventsRoundResults.Select(r => r.meanHeadshotPercentagePrePE).ToList();
        }

        public List<int> GetKASTPost()
        {
            return modelContext.PositiveEventsRoundResults.Select(r => r.meanKASTPostPE).ToList();
        }

        public List<int> GetRoundWinratePost()
        {
            return modelContext.PositiveEventsRoundResults.Select(r => r.roundWinratePostPE).ToList();
        }

        public List<int> GetHeadshotPercentagePost()
        {
            return modelContext.PositiveEventsRoundResults.Select(r => r.meanHeadshotPercentagePostPE).ToList();
        }
    }
}
