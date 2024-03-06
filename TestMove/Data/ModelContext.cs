using TestMove;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMove
{
    internal class ModelContext : DbContext
    {
        // the ModelContext used by EntityFramework to build and store database

        // all tables
        public DbSet<RoundResults> RoundResults { get; set; }
        public DbSet<HPRoundResults> HPRoundResults { get; set; }
        public DbSet<NegativeEventsRoundResults> NegativeEventsRoundResults { get; set; }
        public DbSet<PositiveEventsRoundResults> PositiveEventsRoundResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // specifying a local folder to store the database
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\aloose moose\source\repos\TestMove\TestMove\RoundResults_Database.db");
        }
    }
}
