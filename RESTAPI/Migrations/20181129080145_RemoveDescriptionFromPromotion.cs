using Microsoft.EntityFrameworkCore.Migrations;

namespace RESTAPI.Migrations
{
    public partial class RemoveDescriptionFromPromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Promotion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Promotion",
                nullable: true);
        }
    }
}
