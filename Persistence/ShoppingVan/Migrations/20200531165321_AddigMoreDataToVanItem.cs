using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ShoppingVan.Migrations
{
    public partial class AddigMoreDataToVanItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingVanItems_ShoppingVans_VanId",
                table: "ShoppingVanItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingVanItems_VanId",
                table: "ShoppingVanItems");

            migrationBuilder.AlterColumn<string>(
                name: "VanId",
                table: "ShoppingVanItems",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "ShoppingVanItems",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SellingPrice",
                table: "ShoppingVanItems",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "ShoppingVanItems",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "UnitPrice",
                table: "ShoppingVanItems",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<Guid>(
                name: "VanId1",
                table: "ShoppingVanItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingVanItems_VanId1",
                table: "ShoppingVanItems",
                column: "VanId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingVanItems_ShoppingVans_VanId1",
                table: "ShoppingVanItems",
                column: "VanId1",
                principalTable: "ShoppingVans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingVanItems_ShoppingVans_VanId1",
                table: "ShoppingVanItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingVanItems_VanId1",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "VanId1",
                table: "ShoppingVanItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "VanId",
                table: "ShoppingVanItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingVanItems_VanId",
                table: "ShoppingVanItems",
                column: "VanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingVanItems_ShoppingVans_VanId",
                table: "ShoppingVanItems",
                column: "VanId",
                principalTable: "ShoppingVans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
