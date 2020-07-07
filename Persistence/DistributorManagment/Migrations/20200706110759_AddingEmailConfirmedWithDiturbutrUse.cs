using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.DistributorManagment.Migrations
{
    public partial class AddingEmailConfirmedWithDiturbutrUse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributorUser_Distributors_DistributorId1",
                table: "DistributorUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DistributorUser",
                table: "DistributorUser");

            migrationBuilder.RenameTable(
                name: "DistributorUser",
                newName: "DistributorUsers");

            migrationBuilder.RenameIndex(
                name: "IX_DistributorUser_DistributorId1",
                table: "DistributorUsers",
                newName: "IX_DistributorUsers_DistributorId1");

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "DistributorUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DistributorUsers",
                table: "DistributorUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorUsers_Distributors_DistributorId1",
                table: "DistributorUsers",
                column: "DistributorId1",
                principalTable: "Distributors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributorUsers_Distributors_DistributorId1",
                table: "DistributorUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DistributorUsers",
                table: "DistributorUsers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "DistributorUsers");

            migrationBuilder.RenameTable(
                name: "DistributorUsers",
                newName: "DistributorUser");

            migrationBuilder.RenameIndex(
                name: "IX_DistributorUsers_DistributorId1",
                table: "DistributorUser",
                newName: "IX_DistributorUser_DistributorId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DistributorUser",
                table: "DistributorUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorUser_Distributors_DistributorId1",
                table: "DistributorUser",
                column: "DistributorId1",
                principalTable: "Distributors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
