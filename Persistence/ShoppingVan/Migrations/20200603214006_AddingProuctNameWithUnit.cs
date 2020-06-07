using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ShoppingVan.Migrations
{
    public partial class AddingProuctNameWithUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "ShoppingVanItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "ShoppingVanItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "ShoppingVanItems");
        }
    }
}
