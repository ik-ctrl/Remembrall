using Microsoft.EntityFrameworkCore.Migrations;

namespace DataStorage.Migrations
{
    public partial class Notes_IsDoneAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "IsDone",
                table: "Notes",
                defaultValue: false,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
