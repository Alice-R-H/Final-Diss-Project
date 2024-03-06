using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestMove
{
    // the RoundResults table ModelContext

    public class RoundResults
    {
            [Key]
            public int Id { get; set; }
            
            public int kills { get; set; }
            public int assists { get; set; }
            public bool deaths { get; set; } 
            public bool trades { get; set; }
            public int abilitiesUsed { get; set; }
            public int HeadshotPercentage { get; set; }
            public bool RoundWin { get; set; }
            public int RoundIdentifier { get; set; }
    }
}
