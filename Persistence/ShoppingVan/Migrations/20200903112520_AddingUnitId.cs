using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ShoppingVan.Migrations
{
    public partial class AddingUnitId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "VanItemUnits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "VanItemUnits");
        }
    }
}
