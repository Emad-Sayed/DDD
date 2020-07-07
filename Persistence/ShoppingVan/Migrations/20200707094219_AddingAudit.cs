using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ShoppingVan.Migrations
{
    public partial class AddingAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateUtc",
                table: "ShoppingVans");

            migrationBuilder.DropColumn(
                name: "CreatedDateUtc",
                table: "ShoppingVanItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ShoppingVans",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ShoppingVans",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ShoppingVans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ShoppingVans",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ShoppingVanItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ShoppingVanItems",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ShoppingVanItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ShoppingVanItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "ShoppingVans");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ShoppingVans");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ShoppingVans");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ShoppingVans");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ShoppingVanItems");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ShoppingVanItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateUtc",
                table: "ShoppingVans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateUtc",
                table: "ShoppingVanItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
