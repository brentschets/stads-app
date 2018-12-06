using Microsoft.EntityFrameworkCore.Migrations;

namespace RESTAPI.Migrations
{
    public partial class AddSubscriptionsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Establishment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Establishment_UserId",
                table: "Establishment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Establishment_User_UserId",
                table: "Establishment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Establishment_User_UserId",
                table: "Establishment");

            migrationBuilder.DropIndex(
                name: "IX_Establishment_UserId",
                table: "Establishment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Establishment");
        }
    }
}
