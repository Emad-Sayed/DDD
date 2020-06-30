using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ProductCatalog.Migrations
{
    public partial class RemoveBrandAndProductCategorySeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
