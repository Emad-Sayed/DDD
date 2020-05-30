using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.ProductCatalog.Migrations
{
    public partial class SeedBrandAndProductCategoryData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
