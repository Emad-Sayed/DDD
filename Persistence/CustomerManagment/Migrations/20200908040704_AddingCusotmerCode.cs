using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.CustomerManagment.Migrations
{
    public partial class AddingCusotmerCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.CreateSequence<int>(
                name: "CustomerNumbers",
                schema: "shared",
                startValue: 1000L);

            migrationBuilder.AddColumn<string>(
                name: "CustomerCode",
                table: "Customers",
                nullable: true,
                defaultValueSql: "NEXT VALUE FOR shared.CustomerNumbers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "CustomerNumbers",
                schema: "shared");

            migrationBuilder.DropColumn(
                name: "CustomerCode",
                table: "Customers");
        }
    }
}
