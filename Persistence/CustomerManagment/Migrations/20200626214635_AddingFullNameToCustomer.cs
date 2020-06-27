using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.CustomerManagment.Migrations
{
    public partial class AddingFullNameToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Customers");
        }
    }
}
