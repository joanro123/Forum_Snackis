using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum_Snackis.Migrations
{
    public partial class PrivateMessageNickname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderNickname",
                table: "PrivateMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderNickname",
                table: "PrivateMessages");
        }
    }
}
