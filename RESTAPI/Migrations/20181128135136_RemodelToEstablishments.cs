using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RESTAPI.Migrations
{
    public partial class RemodelToEstablishments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Store_StoreId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Store_Address_AddressId",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_Store_AddressId",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Visited",
                table: "Store");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Event",
                newName: "EstablishmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_StoreId",
                table: "Event",
                newName: "IX_Event_EstablishmentId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Event",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Establishment",
                columns: table => new
                {
                    EstablishmentId = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImgPath = table.Column<string>(nullable: true),
                    Visited = table.Column<int>(),
                    AddressId = table.Column<int>(nullable: true),
                    StoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establishment", x => x.EstablishmentId);
                    table.ForeignKey(
                        name: "FK_Establishment_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Establishment_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Establishment_AddressId",
                table: "Establishment",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Establishment_StoreId",
                table: "Establishment",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Establishment_EstablishmentId",
                table: "Event",
                column: "EstablishmentId",
                principalTable: "Establishment",
                principalColumn: "EstablishmentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Establishment_EstablishmentId",
                table: "Event");

            migrationBuilder.DropTable(
                name: "Establishment");

            migrationBuilder.DropIndex(
                name: "IX_User_Username",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "EstablishmentId",
                table: "Event",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_EstablishmentId",
                table: "Event",
                newName: "IX_Event_StoreId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Store",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Visited",
                table: "Store",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Store_AddressId",
                table: "Store",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Store_StoreId",
                table: "Event",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Address_AddressId",
                table: "Store",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
