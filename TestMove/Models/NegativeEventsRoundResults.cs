using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestMove
{
    // the NegativeEventsRoundResults table ModelContext

    public class NegativeEventsRoundResults
    {
        [Key]
        public int Id { get; set; }

        // preceeding event (PreNE)
        public int meanKASTPreNE { get; set; }
        public int roundWinratePreNE { get; set; }
        public int meanHeadshotPercentagePreNE { get; set; }

        // post event (PostNE)
        public int meanKASTPostNE { get; set; }
        public int roundWinratePostNE { get; set; }
        public int meanHeadshotPercentagePostNE { get; set; }
    }
}
