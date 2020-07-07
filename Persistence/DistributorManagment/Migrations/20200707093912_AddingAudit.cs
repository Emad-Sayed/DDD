using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.DistributorManagment.Migrations
{
    public partial class AddingAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateUtc",
                table: "DistributorUsers");

            migrationBuilder.DropColumn(
                name: "CreatedDateUtc",
                table: "Distributors");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "DistributorUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DistributorUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "DistributorUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "DistributorUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Distributors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Distributors",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Distributors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Distributors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "DistributorUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DistributorUsers");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "DistributorUsers");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "DistributorUsers");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Distributors");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateUtc",
                table: "DistributorUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateUtc",
                table: "Distributors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
