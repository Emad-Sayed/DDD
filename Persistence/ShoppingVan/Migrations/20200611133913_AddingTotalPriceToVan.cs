using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ShoppingVan.Migrations
{
    public partial class AddingTotalPriceToVan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TotalPrice",
                table: "ShoppingVans",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ShoppingVans");
        }
    }
}
