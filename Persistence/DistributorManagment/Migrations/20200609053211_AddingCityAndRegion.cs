using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.DistributorManagment.Migrations
{
    public partial class AddingCityAndRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "LocationOnMap",
                table: "Distributors");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Distributors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Distributors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Region",
                table: "Distributors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistributorUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateUtc = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DistributorId = table.Column<string>(nullable: true),
                    DistributorId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributorUser_Distributors_DistributorId1",
                        column: x => x.DistributorId1,
                        principalTable: "Distributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    CityId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Cities_CityId1",
                        column: x => x.CityId1,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistributorUser_DistributorId1",
                table: "DistributorUser",
                column: "DistributorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CityId1",
                table: "Regions",
                column: "CityId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributorUser");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "Address_Region",
                table: "Distributors");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Distributors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Distributors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationOnMap",
                table: "Distributors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
