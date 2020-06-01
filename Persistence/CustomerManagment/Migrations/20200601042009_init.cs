using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.CustomerManagment.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateUtc = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<string>(nullable: true),
                    ShopName = table.Column<string>(nullable: true),
                    ShopAddress = table.Column<string>(nullable: true),
                    LocationOnMap = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
