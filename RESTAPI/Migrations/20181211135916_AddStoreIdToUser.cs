using Microsoft.EntityFrameworkCore.Migrations;

namespace RESTAPI.Migrations
{
    public partial class AddStoreIdToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_StoreId",
                table: "User",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Store_StoreId",
                table: "User",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Store_StoreId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_StoreId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "User");
        }
    }
}
