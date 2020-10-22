using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StrategyGame.Dal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    CoralPerRound = table.Column<int>(nullable: false),
                    PearlPerRound = table.Column<int>(nullable: false),
                    PopulationOnConstructionFinished = table.Column<int>(nullable: false),
                    GarrisonOnConstructionFinished = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Researches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CoralMultiplier = table.Column<double>(nullable: false),
                    AttackMultiplier = table.Column<double>(nullable: false),
                    DefenseMultiplier = table.Column<double>(nullable: false),
                    TaxMultiplier = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Researches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    TickedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Attack = table.Column<int>(nullable: false),
                    Defense = table.Column<int>(nullable: false),
                    Supply = table.Column<int>(nullable: false),
                    Pay = table.Column<int>(nullable: false),
                    ScoreboardValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Population = table.Column<int>(nullable: false),
                    Pearl = table.Column<int>(nullable: false),
                    Coral = table.Column<int>(nullable: false),
                    Garrison = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Combats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttackerId = table.Column<int>(nullable: false),
                    DefenderId = table.Column<int>(nullable: false),
                    RoundId = table.Column<int>(nullable: false),
                    IsConcluded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Combats_Countries_AttackerId",
                        column: x => x.AttackerId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Combats_Countries_DefenderId",
                        column: x => x.DefenderId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Combats_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryBuildingConnectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(nullable: false),
                    BuildingId = table.Column<int>(nullable: false),
                    BuildStartedRoundId = table.Column<int>(nullable: false),
                    RoundsLeftUntilCompletion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryBuildingConnectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryBuildingConnectors_Rounds_BuildStartedRoundId",
                        column: x => x.BuildStartedRoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryBuildingConnectors_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryBuildingConnectors_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryResearchConnectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(nullable: false),
                    ResearchId = table.Column<int>(nullable: false),
                    ResearchStartedRoundId = table.Column<int>(nullable: false),
                    RoundsLeftUntilCompletion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryResearchConnectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryResearchConnectors_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryResearchConnectors_Researches_ResearchId",
                        column: x => x.ResearchId,
                        principalTable: "Researches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryResearchConnectors_Rounds_ResearchStartedRoundId",
                        column: x => x.ResearchStartedRoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryUnitConnectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    TotalCount = table.Column<int>(nullable: false),
                    InCombat = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryUnitConnectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryUnitConnectors_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryUnitConnectors_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoreboardEntries",
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
                    table.PrimaryKey("PK_ScoreboardEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoreboardEntries_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoreboardEntries_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombatUnitConnectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(nullable: false),
                    CombatId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatUnitConnectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatUnitConnectors_Combats_CombatId",
                        column: x => x.CombatId,
                        principalTable: "Combats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombatUnitConnectors_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "CoralPerRound", "Description", "GarrisonOnConstructionFinished", "Name", "PearlPerRound", "PopulationOnConstructionFinished", "Price" },
                values: new object[,]
                {
                    { 1, 200, null, 0, "Flow controller", 0, 50, 1000 },
                    { 2, 0, null, 200, "Reef castle", 0, 0, 1000 }
                });

            migrationBuilder.InsertData(
                table: "Researches",
                columns: new[] { "Id", "AttackMultiplier", "CoralMultiplier", "DefenseMultiplier", "Description", "Name", "Price", "TaxMultiplier" },
                values: new object[,]
                {
                    { 1, 1.0, 1.1000000000000001, 1.0, null, "Mud tractor", 300, 1.0 },
                    { 2, 1.0, 1.1499999999999999, 1.0, null, "Mud harvester", 500, 1.0 },
                    { 3, 1.0, 1.0, 1.2, null, "Coral wall", 500, 1.0 },
                    { 4, 1.2, 1.0, 1.0, null, "Sonar cannon", 500, 1.0 },
                    { 5, 1.1000000000000001, 1.0, 1.1000000000000001, null, "Underwater martial arts", 750, 1.0 },
                    { 6, 1.0, 1.0, 1.0, null, "Alchemy", 1000, 1.3 }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Attack", "Defense", "Description", "Name", "Pay", "Price", "ScoreboardValue", "Supply" },
                values: new object[,]
                {
                    { 1, 6, 2, null, "Rush seal", 1, 50, 5, 1 },
                    { 2, 2, 6, null, "War sea horse", 1, 50, 5, 1 },
                    { 3, 5, 5, null, "Laser shark", 3, 100, 10, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Combats_AttackerId",
                table: "Combats",
                column: "AttackerId");

            migrationBuilder.CreateIndex(
                name: "IX_Combats_DefenderId",
                table: "Combats",
                column: "DefenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Combats_RoundId",
                table: "Combats",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatUnitConnectors_CombatId",
                table: "CombatUnitConnectors",
                column: "CombatId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatUnitConnectors_UnitId",
                table: "CombatUnitConnectors",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_UserId",
                table: "Countries",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryBuildingConnectors_BuildStartedRoundId",
                table: "CountryBuildingConnectors",
                column: "BuildStartedRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryBuildingConnectors_BuildingId",
                table: "CountryBuildingConnectors",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryBuildingConnectors_CountryId",
                table: "CountryBuildingConnectors",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryResearchConnectors_CountryId",
                table: "CountryResearchConnectors",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryResearchConnectors_ResearchId",
                table: "CountryResearchConnectors",
                column: "ResearchId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryResearchConnectors_ResearchStartedRoundId",
                table: "CountryResearchConnectors",
                column: "ResearchStartedRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryUnitConnectors_CountryId",
                table: "CountryUnitConnectors",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryUnitConnectors_UnitId",
                table: "CountryUnitConnectors",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreboardEntries_CountryId",
                table: "ScoreboardEntries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreboardEntries_RoundId",
                table: "ScoreboardEntries",
                column: "RoundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CombatUnitConnectors");

            migrationBuilder.DropTable(
                name: "CountryBuildingConnectors");

            migrationBuilder.DropTable(
                name: "CountryResearchConnectors");

            migrationBuilder.DropTable(
                name: "CountryUnitConnectors");

            migrationBuilder.DropTable(
                name: "ScoreboardEntries");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Combats");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Researches");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
