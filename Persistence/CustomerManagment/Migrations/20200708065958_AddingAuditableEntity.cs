using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.CustomerManagment.Migrations
{
    public partial class AddingAuditableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Areas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Cities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Cities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Areas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Areas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Areas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Areas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
