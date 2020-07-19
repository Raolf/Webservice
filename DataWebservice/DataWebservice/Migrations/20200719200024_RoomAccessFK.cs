using Microsoft.EntityFrameworkCore.Migrations;

namespace DataWebservice.Migrations
{
    public partial class RoomAccessFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_User_userID",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_userID",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "userID",
                table: "Room");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userID",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Room_userID",
                table: "Room",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_User_userID",
                table: "Room",
                column: "userID",
                principalTable: "User",
                principalColumn: "userID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
