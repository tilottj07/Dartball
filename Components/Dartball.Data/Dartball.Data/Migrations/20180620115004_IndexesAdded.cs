using Microsoft.EntityFrameworkCore.Migrations;

namespace Dartball.Data.Migrations
{
    public partial class IndexesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlayerTeams_PlayerTeamId",
                table: "PlayerTeams",
                column: "PlayerTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTeams_GameTeamId",
                table: "GameTeams",
                column: "GameTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerTeams_PlayerTeamId",
                table: "PlayerTeams");

            migrationBuilder.DropIndex(
                name: "IX_GameTeams_GameTeamId",
                table: "GameTeams");
        }
    }
}
