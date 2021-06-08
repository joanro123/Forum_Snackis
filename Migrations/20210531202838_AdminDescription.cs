using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum_Snackis.Migrations
{
    public partial class AdminDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AdminFunctions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AdminFunctions");
        }
    }
}
