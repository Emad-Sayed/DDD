using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ShoppingVan.Migrations
{
    public partial class AddingAllProductUnitsToShoppingVan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "ShoppingVanItems");

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    CustomerCount = table.Column<int>(nullable: false),
                    ContentCount = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    SellingPrice = table.Column<float>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    VanItemId = table.Column<string>(nullable: true),
                    VanItemId1 = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unit_ShoppingVanItems_VanItemId1",
                        column: x => x.VanItemId1,
                        principalTable: "ShoppingVanItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Unit_VanItemId1",
                table: "Unit",
                column: "VanItemId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "ShoppingVanItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "SellingPrice",
                table: "ShoppingVanItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "ShoppingVanItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "ShoppingVanItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "UnitPrice",
                table: "ShoppingVanItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
