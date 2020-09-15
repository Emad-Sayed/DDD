using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.DistributorManagment.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address_Area = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistributorsCities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorsCities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistributorUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    AccountId = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    DistributorId = table.Column<string>(nullable: true),
                    DistributorId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributorUsers_Distributors_DistributorId1",
                        column: x => x.DistributorId1,
                        principalTable: "Distributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DistributorsAreas",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorsAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributorsAreas_DistributorsCities_CityId",
                        column: x => x.CityId,
                        principalTable: "DistributorsCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DistributorArea",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AreaId = table.Column<string>(nullable: true),
                    DistributorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributorArea_DistributorsAreas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "DistributorsAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributorArea_Distributors_DistributorId",
                        column: x => x.DistributorId,
                        principalTable: "Distributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistributorArea_AreaId",
                table: "DistributorArea",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorArea_DistributorId",
                table: "DistributorArea",
                column: "DistributorId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorsAreas_CityId",
                table: "DistributorsAreas",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorUsers_DistributorId1",
                table: "DistributorUsers",
                column: "DistributorId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributorArea");

            migrationBuilder.DropTable(
                name: "DistributorUsers");

            migrationBuilder.DropTable(
                name: "DistributorsAreas");

            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropTable(
                name: "DistributorsCities");
        }
    }
}
