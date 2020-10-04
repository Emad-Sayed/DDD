using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.OrderManagment.Migrations
{
    public partial class AddingDistributorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistributorId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistributorName",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistributorId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DistributorName",
                table: "Orders");
        }
    }
}
