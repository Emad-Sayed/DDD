using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ShoppingVan.Migrations
{
    public partial class FixInUnitsTabl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_ShoppingVanItems_VanItemId1",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "VanItemUnits");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_VanItemId1",
                table: "VanItemUnits",
                newName: "IX_VanItemUnits_VanItemId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VanItemUnits",
                table: "VanItemUnits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VanItemUnits_ShoppingVanItems_VanItemId1",
                table: "VanItemUnits",
                column: "VanItemId1",
                principalTable: "ShoppingVanItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VanItemUnits_ShoppingVanItems_VanItemId1",
                table: "VanItemUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VanItemUnits",
                table: "VanItemUnits");

            migrationBuilder.RenameTable(
                name: "VanItemUnits",
                newName: "Unit");

            migrationBuilder.RenameIndex(
                name: "IX_VanItemUnits_VanItemId1",
                table: "Unit",
                newName: "IX_Unit_VanItemId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_ShoppingVanItems_VanItemId1",
                table: "Unit",
                column: "VanItemId1",
                principalTable: "ShoppingVanItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
