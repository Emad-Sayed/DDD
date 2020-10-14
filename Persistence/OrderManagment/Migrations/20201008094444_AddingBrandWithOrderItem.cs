using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.OrderManagment.Migrations
{
    public partial class AddingBrandWithOrderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "OrderItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "OrderItems");
        }
    }
}
