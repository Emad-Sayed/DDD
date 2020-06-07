using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.OrderManagment.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateUtc = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<int>(nullable: false),
                    OrderPlacedDate = table.Column<DateTime>(nullable: false),
                    OrderConfirmedDate = table.Column<DateTime>(nullable: false),
                    OrderShippedDate = table.Column<DateTime>(nullable: false),
                    OrderDeliveredDate = table.Column<DateTime>(nullable: false),
                    OrderCanceledDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateUtc = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<float>(nullable: false),
                    UnitId = table.Column<string>(nullable: true),
                    UnitName = table.Column<string>(nullable: true),
                    UnitCount = table.Column<int>(nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    OrderId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId1",
                table: "OrderItems",
                column: "OrderId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
