using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestMove
{
    // the PositiveEventsRoundResults table ModelContext

    public class PositiveEventsRoundResults
    {
        [Key]
        public int Id { get; set; }

        // preceeding event (PrePE)
        public int meanKASTPrePE { get; set; }        
        public int meanHeadshotPercentagePrePE { get; set; }
        public int roundWinratePrePE { get; set; }

        // post event (PostPE)
        public int meanKASTPostPE { get; set; }        
        public int meanHeadshotPercentagePostPE { get; set; }
        public int roundWinratePostPE { get; set; }
    }
}
