using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMove.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HPRoundResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    killsHP = table.Column<int>(type: "INTEGER", nullable: false),
                    assistsHP = table.Column<int>(type: "INTEGER", nullable: false),
                    deathsHP = table.Column<bool>(type: "INTEGER", nullable: false),
                    tradesHP = table.Column<bool>(type: "INTEGER", nullable: false),
                    HeadshotPercentageHP = table.Column<int>(type: "INTEGER", nullable: false),
                    RoundWinHP = table.Column<bool>(type: "INTEGER", nullable: false),
                    RoundIdentifierHP = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HPRoundResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NegativeEventsRoundResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    meanKASTPreNE = table.Column<int>(type: "INTEGER", nullable: false),
                    roundWinratePreNE = table.Column<int>(type: "INTEGER", nullable: false),
                    meanHeadshotPercentagePreNE = table.Column<int>(type: "INTEGER", nullable: false),
                    meanKASTPostNE = table.Column<int>(type: "INTEGER", nullable: false),
                    roundWinratePostNE = table.Column<int>(type: "INTEGER", nullable: false),
                    meanHeadshotPercentagePostNE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegativeEventsRoundResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PositiveEventsRoundResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    meanKASTPrePE = table.Column<int>(type: "INTEGER", nullable: false),
                    meanHeadshotPercentagePrePE = table.Column<int>(type: "INTEGER", nullable: false),
                    roundWinratePrePE = table.Column<int>(type: "INTEGER", nullable: false),
                    meanKASTPostPE = table.Column<int>(type: "INTEGER", nullable: false),
                    meanHeadshotPercentagePostPE = table.Column<int>(type: "INTEGER", nullable: false),
                    roundWinratePostPE = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositiveEventsRoundResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoundResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    kills = table.Column<int>(type: "INTEGER", nullable: false),
                    assists = table.Column<int>(type: "INTEGER", nullable: false),
                    deaths = table.Column<bool>(type: "INTEGER", nullable: false),
                    trades = table.Column<bool>(type: "INTEGER", nullable: false),
                    HeadshotPercentage = table.Column<int>(type: "INTEGER", nullable: false),
                    RoundWin = table.Column<bool>(type: "INTEGER", nullable: false),
                    RoundIdentifier = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HPRoundResults");

            migrationBuilder.DropTable(
                name: "NegativeEventsRoundResults");

            migrationBuilder.DropTable(
                name: "PositiveEventsRoundResults");

            migrationBuilder.DropTable(
                name: "RoundResults");
        }
    }
}
