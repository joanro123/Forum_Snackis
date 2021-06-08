using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum_Snackis.Migrations
{
    public partial class PrivateMessageUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PrivateMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PrivateMessages");
        }
    }
}
