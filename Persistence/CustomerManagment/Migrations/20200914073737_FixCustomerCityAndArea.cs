using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.CustomerManagment.Migrations
{
    public partial class FixCustomerCityAndArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Cities_CityId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Areas_AreaId",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "CustomersCities");

            migrationBuilder.RenameTable(
                name: "Areas",
                newName: "CustomersAreas");

            migrationBuilder.RenameIndex(
                name: "IX_Areas_CityId",
                table: "CustomersAreas",
                newName: "IX_CustomersAreas_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomersCities",
                table: "CustomersCities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomersAreas",
                table: "CustomersAreas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomersAreas_AreaId",
                table: "Customers",
                column: "AreaId",
                principalTable: "CustomersAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersAreas_CustomersCities_CityId",
                table: "CustomersAreas",
                column: "CityId",
                principalTable: "CustomersCities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomersAreas_AreaId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomersAreas_CustomersCities_CityId",
                table: "CustomersAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomersCities",
                table: "CustomersCities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomersAreas",
                table: "CustomersAreas");

            migrationBuilder.RenameTable(
                name: "CustomersCities",
                newName: "Cities");

            migrationBuilder.RenameTable(
                name: "CustomersAreas",
                newName: "Areas");

            migrationBuilder.RenameIndex(
                name: "IX_CustomersAreas_CityId",
                table: "Areas",
                newName: "IX_Areas_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Cities_CityId",
                table: "Areas",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Areas_AreaId",
                table: "Customers",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
