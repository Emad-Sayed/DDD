using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.OfferManagment.Migrations
{
    public partial class AddingUnitId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "OffersProductsUnits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "OffersProductsUnits");
        }
    }
}
