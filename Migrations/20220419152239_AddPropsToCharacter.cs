using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trackr.Migrations
{
    public partial class AddPropsToCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Guild",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Race",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Guild",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "Characters");
        }
    }
}
