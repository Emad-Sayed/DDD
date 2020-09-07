using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.OrderManagment.Migrations
{
    public partial class AddingOrderNunmber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.CreateSequence<int>(
                name: "OrderNumbers",
                schema: "shared",
                startValue: 1000L);

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "Orders",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR shared.OrderNumbers");

            migrationBuilder.AddColumn<float>(
                name: "UnitSellingPrice",
                table: "OrderItems",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "OrderNumbers",
                schema: "shared");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UnitSellingPrice",
                table: "OrderItems");
        }
    }
}
