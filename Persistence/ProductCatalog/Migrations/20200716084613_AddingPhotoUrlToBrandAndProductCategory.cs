using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ProductCatalog.Migrations
{
    public partial class AddingPhotoUrlToBrandAndProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "ProductCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Brands");
        }
    }
}
