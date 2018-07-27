using Microsoft.EntityFrameworkCore.Migrations;

namespace Dartball.Data.Migrations
{
    public partial class PlayerTableFirstNameLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Players",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Players",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Players",
                newName: "Name");
        }
    }
}
