﻿using System;
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
                    LeagueId = table.Column<int>(nullable: false),
                    LeagueAlternateKey = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.LeagueAlternateKey);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    PlayerAlternateKey = table.Column<string>(nullable: false),
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
                    table.PrimaryKey("PK_Players", x => x.PlayerAlternateKey);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    GameAlternateKey = table.Column<string>(nullable: false),
                    LeagueAlternateKey = table.Column<string>(nullable: true),
                    GameDate = table.Column<DateTime>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameAlternateKey);
                    table.ForeignKey(
                        name: "FK_Games_Leagues_LeagueAlternateKey",
                        column: x => x.LeagueAlternateKey,
                        principalTable: "Leagues",
                        principalColumn: "LeagueAlternateKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false),
                    TeamAlternateKey = table.Column<string>(nullable: false),
                    LeagueAlternateKey = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Handicap = table.Column<int>(nullable: true),
                    ShouldSync = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamAlternateKey);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueAlternateKey",
                        column: x => x.LeagueAlternateKey,
                        principalTable: "Leagues",
                        principalColumn: "LeagueAlternateKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameInnings",
                columns: table => new
                {
                    GameInningId = table.Column<int>(nullable: false),
                    GameInningAlternateKey = table.Column<string>(nullable: false),
                    GameAlternateKey = table.Column<string>(nullable: true),
                    InningNumber = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInnings", x => x.GameInningAlternateKey);
                    table.ForeignKey(
                        name: "FK_GameInnings_Games_GameAlternateKey",
                        column: x => x.GameAlternateKey,
                        principalTable: "Games",
                        principalColumn: "GameAlternateKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameTeams",
                columns: table => new
                {
                    GameTeamId = table.Column<int>(nullable: false),
                    GameTeamAlternateKey = table.Column<string>(nullable: true),
                    GameAlternateKey = table.Column<string>(nullable: false),
                    TeamAlternateKey = table.Column<string>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTeams", x => new { x.GameAlternateKey, x.TeamAlternateKey });
                    table.ForeignKey(
                        name: "FK_GameTeams_Games_GameAlternateKey",
                        column: x => x.GameAlternateKey,
                        principalTable: "Games",
                        principalColumn: "GameAlternateKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameTeams_Teams_TeamAlternateKey",
                        column: x => x.TeamAlternateKey,
                        principalTable: "Teams",
                        principalColumn: "TeamAlternateKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerTeams",
                columns: table => new
                {
                    PlayerTeamId = table.Column<int>(nullable: false),
                    PlayerTeamAlternateKey = table.Column<string>(nullable: true),
                    PlayerAlternateKey = table.Column<string>(nullable: false),
                    TeamAlternateKey = table.Column<string>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTeams", x => new { x.PlayerAlternateKey, x.TeamAlternateKey });
                    table.ForeignKey(
                        name: "FK_PlayerTeams_Players_PlayerAlternateKey",
                        column: x => x.PlayerAlternateKey,
                        principalTable: "Players",
                        principalColumn: "PlayerAlternateKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerTeams_Teams_TeamAlternateKey",
                        column: x => x.TeamAlternateKey,
                        principalTable: "Teams",
                        principalColumn: "TeamAlternateKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayerLineups",
                columns: table => new
                {
                    TeamPlayerLineupId = table.Column<int>(nullable: false),
                    TeamPlayerLineupAlternateKey = table.Column<string>(nullable: true),
                    TeamAlternateKey = table.Column<string>(nullable: false),
                    PlayerAlternateKey = table.Column<string>(nullable: false),
                    BattingOrder = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayerLineups", x => new { x.TeamAlternateKey, x.PlayerAlternateKey });
                    table.ForeignKey(
                        name: "FK_TeamPlayerLineups_Players_PlayerAlternateKey",
                        column: x => x.PlayerAlternateKey,
                        principalTable: "Players",
                        principalColumn: "PlayerAlternateKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamPlayerLineups_Teams_TeamAlternateKey",
                        column: x => x.TeamAlternateKey,
                        principalTable: "Teams",
                        principalColumn: "TeamAlternateKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameInningTeams",
                columns: table => new
                {
                    GameInningTeamId = table.Column<int>(nullable: false),
                    GameInningTeamAlternateKey = table.Column<string>(nullable: false),
                    GameInningAlternateKey = table.Column<string>(nullable: true),
                    GameTeamAlternateKey = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_GameInningTeams", x => x.GameInningTeamAlternateKey);
                    table.ForeignKey(
                        name: "FK_GameInningTeams_GameInnings_GameInningAlternateKey",
                        column: x => x.GameInningAlternateKey,
                        principalTable: "GameInnings",
                        principalColumn: "GameInningAlternateKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameInningTeamBatters",
                columns: table => new
                {
                    GameInningTeamBatterId = table.Column<int>(nullable: false),
                    GameInningTeamBatterAlternateKey = table.Column<string>(nullable: false),
                    GameInningTeamAlternateKey = table.Column<string>(nullable: true),
                    PlayerAlternateKey = table.Column<string>(nullable: true),
                    Sequence = table.Column<int>(nullable: false),
                    EventType = table.Column<int>(nullable: false),
                    TargetEventType = table.Column<int>(nullable: true),
                    RBIs = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInningTeamBatters", x => x.GameInningTeamBatterAlternateKey);
                    table.ForeignKey(
                        name: "FK_GameInningTeamBatters_GameInningTeams_GameInningTeamAlternateKey",
                        column: x => x.GameInningTeamAlternateKey,
                        principalTable: "GameInningTeams",
                        principalColumn: "GameInningTeamAlternateKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameInningTeamBatters_Players_PlayerAlternateKey",
                        column: x => x.PlayerAlternateKey,
                        principalTable: "Players",
                        principalColumn: "PlayerAlternateKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameInnings_GameAlternateKey_InningNumber",
                table: "GameInnings",
                columns: new[] { "GameAlternateKey", "InningNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_GameInningTeamBatters_GameInningTeamAlternateKey",
                table: "GameInningTeamBatters",
                column: "GameInningTeamAlternateKey");

            migrationBuilder.CreateIndex(
                name: "IX_GameInningTeamBatters_PlayerAlternateKey",
                table: "GameInningTeamBatters",
                column: "PlayerAlternateKey");

            migrationBuilder.CreateIndex(
                name: "IX_GameInningTeams_GameInningAlternateKey_GameTeamAlternateKey",
                table: "GameInningTeams",
                columns: new[] { "GameInningAlternateKey", "GameTeamAlternateKey" });

            migrationBuilder.CreateIndex(
                name: "IX_Games_LeagueAlternateKey",
                table: "Games",
                column: "LeagueAlternateKey");

            migrationBuilder.CreateIndex(
                name: "IX_GameTeams_TeamAlternateKey",
                table: "GameTeams",
                column: "TeamAlternateKey");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTeams_TeamAlternateKey",
                table: "PlayerTeams",
                column: "TeamAlternateKey");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayerLineups_PlayerAlternateKey",
                table: "TeamPlayerLineups",
                column: "PlayerAlternateKey");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueAlternateKey",
                table: "Teams",
                column: "LeagueAlternateKey");
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