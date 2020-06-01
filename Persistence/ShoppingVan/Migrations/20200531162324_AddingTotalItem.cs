using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ShoppingVan.Migrations
{
    public partial class AddingTotalItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingVanItems_ShoppingVans_ShoppingVanId",
                table: "ShoppingVanItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingVanItems_ShoppingVanId",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "ShoppingVanId",
                table: "ShoppingVanItems");

            migrationBuilder.AddColumn<int>(
                name: "TotalItems",
                table: "ShoppingVans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "VanId",
                table: "ShoppingVanItems",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingVanItems_ShoppingVans_VanId",
                table: "ShoppingVanItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingVanItems_VanId",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "TotalItems",
                table: "ShoppingVans");

            migrationBuilder.DropColumn(
                name: "VanId",
                table: "ShoppingVanItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingVanId",
                table: "ShoppingVanItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingVanItems_ShoppingVanId",
                table: "ShoppingVanItems",
                column: "ShoppingVanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingVanItems_ShoppingVans_ShoppingVanId",
                table: "ShoppingVanItems",
                column: "ShoppingVanId",
                principalTable: "ShoppingVans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
