using Microsoft.EntityFrameworkCore.Migrations;

namespace StrategyGame.Dal.Migrations
{
    public partial class AddedScoreboardEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoreboardEntry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(nullable: false),
                    PopulationScore = table.Column<int>(nullable: false),
                    BuildingScore = table.Column<int>(nullable: false),
                    ArmyScore = table.Column<int>(nullable: false),
                    ResearchScore = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    RoundId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreboardEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoreboardEntry_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoreboardEntry_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoreboardEntry_CountryId",
                table: "ScoreboardEntry",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreboardEntry_RoundId",
                table: "ScoreboardEntry",
                column: "RoundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoreboardEntry");
        }
    }
}
