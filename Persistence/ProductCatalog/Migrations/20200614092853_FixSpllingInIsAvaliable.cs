using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ProductCatalog.Migrations
{
    public partial class FixSpllingInIsAvaliable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("950f5ce4-b5e5-46e6-8e20-3f2b750da635"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("dec04667-6960-4842-9002-0db125500ddd"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("213395b5-7a8e-42b7-bbb3-05db816e016f"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("db0e795e-5a01-4a19-9814-8b8d24e64202"));

            migrationBuilder.DropColumn(
                name: "IsAvilable",
                table: "Units");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Units",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedDateUtc", "Name" },
                values: new object[,]
                {
                    { new Guid("9de2a9e6-867e-4051-9c20-98f97699c697"), new DateTime(2020, 6, 14, 9, 28, 52, 514, DateTimeKind.Utc).AddTicks(5689), "Brand 1" },
                    { new Guid("1ffd55cc-be56-46c5-8532-2726f878fc71"), new DateTime(2020, 6, 14, 9, 28, 52, 514, DateTimeKind.Utc).AddTicks(7802), "Brand 2" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "CreatedDateUtc", "Name" },
                values: new object[,]
                {
                    { new Guid("460ef8c6-22c4-4a4d-9c20-8ef253ff5c3b"), new DateTime(2020, 6, 14, 9, 28, 52, 516, DateTimeKind.Utc).AddTicks(5428), "ProductCategory 1" },
                    { new Guid("509c00ae-6ec9-4cff-8a90-41c3593ed395"), new DateTime(2020, 6, 14, 9, 28, 52, 516, DateTimeKind.Utc).AddTicks(6205), "ProductCategory 2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("1ffd55cc-be56-46c5-8532-2726f878fc71"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9de2a9e6-867e-4051-9c20-98f97699c697"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("460ef8c6-22c4-4a4d-9c20-8ef253ff5c3b"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("509c00ae-6ec9-4cff-8a90-41c3593ed395"));

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Units");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvilable",
                table: "Units",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedDateUtc", "Name" },
                values: new object[,]
                {
                    { new Guid("dec04667-6960-4842-9002-0db125500ddd"), new DateTime(2020, 5, 30, 13, 53, 11, 744, DateTimeKind.Utc).AddTicks(9732), "Brand 1" },
                    { new Guid("950f5ce4-b5e5-46e6-8e20-3f2b750da635"), new DateTime(2020, 5, 30, 13, 53, 11, 745, DateTimeKind.Utc).AddTicks(4094), "Brand 2" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "CreatedDateUtc", "Name" },
                values: new object[,]
                {
                    { new Guid("db0e795e-5a01-4a19-9814-8b8d24e64202"), new DateTime(2020, 5, 30, 13, 53, 11, 748, DateTimeKind.Utc).AddTicks(8226), "ProductCategory 1" },
                    { new Guid("213395b5-7a8e-42b7-bbb3-05db816e016f"), new DateTime(2020, 5, 30, 13, 53, 11, 748, DateTimeKind.Utc).AddTicks(9755), "ProductCategory 2" }
                });
        }
    }
}
