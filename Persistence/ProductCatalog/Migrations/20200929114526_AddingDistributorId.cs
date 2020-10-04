using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ProductCatalog.Migrations
{
    public partial class AddingDistributorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistributorId",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistributorId",
                table: "Products");
        }
    }
}
