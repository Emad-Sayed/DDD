using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.OfferManagment.Migrations
{
    public partial class FixInOfferContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Offers_OfferId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Product_ProductId",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "OffersProductsUnits");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "OffersProducts");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_ProductId",
                table: "OffersProductsUnits",
                newName: "IX_OffersProductsUnits_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_OfferId",
                table: "OffersProducts",
                newName: "IX_OffersProducts_OfferId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OffersProductsUnits",
                table: "OffersProductsUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OffersProducts",
                table: "OffersProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OffersProducts_Offers_OfferId",
                table: "OffersProducts",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OffersProductsUnits_OffersProducts_ProductId",
                table: "OffersProductsUnits",
                column: "ProductId",
                principalTable: "OffersProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffersProducts_Offers_OfferId",
                table: "OffersProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OffersProductsUnits_OffersProducts_ProductId",
                table: "OffersProductsUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OffersProductsUnits",
                table: "OffersProductsUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OffersProducts",
                table: "OffersProducts");

            migrationBuilder.RenameTable(
                name: "OffersProductsUnits",
                newName: "Unit");

            migrationBuilder.RenameTable(
                name: "OffersProducts",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_OffersProductsUnits_ProductId",
                table: "Unit",
                newName: "IX_Unit_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OffersProducts_OfferId",
                table: "Product",
                newName: "IX_Product_OfferId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Offers_OfferId",
                table: "Product",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Product_ProductId",
                table: "Unit",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
