using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestMove
{
    // the HPRoundResults table ModelContext

    public class HPRoundResults
    {
            [Key]
            public int Id {  get; set; }

            public int killsHP { get; set; }
            public int assistsHP { get; set; }
            public bool deathsHP { get; set; }
            public bool tradesHP { get; set; }
            public int HeadshotPercentageHP { get; set; }
            public bool RoundWinHP { get; set; }
            public int RoundIdentifierHP { get; set; }        
    }
}
