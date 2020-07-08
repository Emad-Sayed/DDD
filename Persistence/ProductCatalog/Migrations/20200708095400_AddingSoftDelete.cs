using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ProductCatalog.Migrations
{
    public partial class AddingSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Units",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");
        }
    }
}
