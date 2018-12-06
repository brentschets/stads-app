using Microsoft.EntityFrameworkCore.Migrations;

namespace RESTAPI.Migrations
{
    public partial class ReplaceSubscriptionsWithJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "UserEstablishment",
                columns: table => new
                {
                    UserId = table.Column<int>(),
                    EstablishmentId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEstablishment", x => new { x.UserId, x.EstablishmentId });
                    table.ForeignKey(
                        name: "FK_UserEstablishment_Establishment_EstablishmentId",
                        column: x => x.EstablishmentId,
                        principalTable: "Establishment",
                        principalColumn: "EstablishmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEstablishment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEstablishment_EstablishmentId",
                table: "UserEstablishment",
                column: "EstablishmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEstablishment");

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
    }
}
