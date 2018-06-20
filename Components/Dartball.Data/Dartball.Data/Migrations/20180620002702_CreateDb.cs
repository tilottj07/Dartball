using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dartball.Data.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    LeagueId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.LeagueId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ShouldSync = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<string>(nullable: false),
                    LeagueId = table.Column<string>(nullable: true),
                    GameDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<string>(nullable: false),
                    LeagueId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Handicap = table.Column<int>(nullable: true),
                    ShouldSync = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameInnings",
                columns: table => new
                {
                    GameInningId = table.Column<string>(nullable: false),
                    GameId = table.Column<string>(nullable: true),
                    InningNumber = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInnings", x => x.GameInningId);
                    table.ForeignKey(
                        name: "FK_GameInnings_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameTeams",
                columns: table => new
                {
                    GameTeamId = table.Column<string>(nullable: true),
                    GameId = table.Column<string>(nullable: false),
                    TeamId = table.Column<string>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTeams", x => new { x.GameId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_GameTeams_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerTeams",
                columns: table => new
                {
                    PlayerTeamId = table.Column<string>(nullable: true),
                    PlayerId = table.Column<string>(nullable: false),
                    TeamId = table.Column<string>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTeams", x => new { x.PlayerId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_PlayerTeams_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayerLineups",
                columns: table => new
                {
                    TeamPlayerLineupId = table.Column<string>(nullable: true),
                    TeamId = table.Column<string>(nullable: false),
                    PlayerId = table.Column<string>(nullable: false),
                    BattingOrder = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayerLineups", x => new { x.TeamId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_TeamPlayerLineups_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamPlayerLineups_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameInningTeams",
                columns: table => new
                {
                    GameInningTeamId = table.Column<string>(nullable: false),
                    GameInningId = table.Column<string>(nullable: true),
                    GameTeamId = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    Outs = table.Column<int>(nullable: false),
                    IsRunnerOnFirst = table.Column<int>(nullable: false),
                    IsRunnerOnSecond = table.Column<int>(nullable: false),
                    IsRunnerOnThird = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInningTeams", x => x.GameInningTeamId);
                    table.ForeignKey(
                        name: "FK_GameInningTeams_GameInnings_GameInningId",
                        column: x => x.GameInningId,
                        principalTable: "GameInnings",
                        principalColumn: "GameInningId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameInningTeamBatters",
                columns: table => new
                {
                    GameInningTeamBatterId = table.Column<string>(nullable: false),
                    GameInningTeamId = table.Column<string>(nullable: true),
                    PlayerId = table.Column<string>(nullable: true),
                    Sequence = table.Column<int>(nullable: false),
                    EventType = table.Column<int>(nullable: false),
                    TargetEventType = table.Column<int>(nullable: true),
                    RBIs = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInningTeamBatters", x => x.GameInningTeamBatterId);
                    table.ForeignKey(
                        name: "FK_GameInningTeamBatters_GameInningTeams_GameInningTeamId",
                        column: x => x.GameInningTeamId,
                        principalTable: "GameInningTeams",
                        principalColumn: "GameInningTeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameInningTeamBatters_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameInnings_GameId_InningNumber",
                table: "GameInnings",
                columns: new[] { "GameId", "InningNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_GameInningTeamBatters_GameInningTeamId",
                table: "GameInningTeamBatters",
                column: "GameInningTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_GameInningTeamBatters_PlayerId",
                table: "GameInningTeamBatters",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameInningTeams_GameInningId_GameTeamId",
                table: "GameInningTeams",
                columns: new[] { "GameInningId", "GameTeamId" });

            migrationBuilder.CreateIndex(
                name: "IX_Games_LeagueId",
                table: "Games",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTeams_TeamId",
                table: "GameTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTeams_TeamId",
                table: "PlayerTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayerLineups_PlayerId",
                table: "TeamPlayerLineups",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameInningTeamBatters");

            migrationBuilder.DropTable(
                name: "GameTeams");

            migrationBuilder.DropTable(
                name: "PlayerTeams");

            migrationBuilder.DropTable(
                name: "TeamPlayerLineups");

            migrationBuilder.DropTable(
                name: "GameInningTeams");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "GameInnings");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
