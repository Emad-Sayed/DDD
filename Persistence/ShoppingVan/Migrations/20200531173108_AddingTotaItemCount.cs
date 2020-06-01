using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ShoppingVan.Migrations
{
    public partial class AddingTotaItemCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalItems",
                table: "ShoppingVans");

            migrationBuilder.AddColumn<int>(
                name: "TotalItemsCount",
                table: "ShoppingVans",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalItemsCount",
                table: "ShoppingVans");

            migrationBuilder.AddColumn<int>(
                name: "TotalItems",
                table: "ShoppingVans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
