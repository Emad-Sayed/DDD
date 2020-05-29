using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ShoppingVan.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingVans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateUtc = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingVans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingVanItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateUtc = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    ShoppingVanId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingVanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingVanItems_ShoppingVans_ShoppingVanId",
                        column: x => x.ShoppingVanId,
                        principalTable: "ShoppingVans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingVanItems_ShoppingVanId",
                table: "ShoppingVanItems",
                column: "ShoppingVanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingVanItems");

            migrationBuilder.DropTable(
                name: "ShoppingVans");
        }
    }
}
