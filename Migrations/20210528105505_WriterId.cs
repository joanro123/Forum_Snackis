using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum_Snackis.Migrations
{
    public partial class WriterId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Writer",
                table: "Threads");

            migrationBuilder.AddColumn<int>(
                name: "WriterId",
                table: "Threads",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "Threads");

            migrationBuilder.AddColumn<string>(
                name: "Writer",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
