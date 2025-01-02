using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPPURCHASES.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration343jkjgfhgfhfghfgfhfghfghDSSDFSDF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ma_Subgroup_Or_Maingroup_maingroup_id",
                table: "Ma_Subgroup");

            migrationBuilder.DropIndex(
                name: "IX_Ma_Subgroup_maingroup_id",
                table: "Ma_Subgroup");

            migrationBuilder.DropColumn(
                name: "maingroup_id",
                table: "Ma_Subgroup");

            migrationBuilder.AddColumn<Guid>(
                name: "Or_MaingroupId",
                table: "Ma_Subgroup",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ma_Subgroup_Or_MaingroupId",
                table: "Ma_Subgroup",
                column: "Or_MaingroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ma_Subgroup_Or_Maingroup_Or_MaingroupId",
                table: "Ma_Subgroup",
                column: "Or_MaingroupId",
                principalTable: "Or_Maingroup",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ma_Subgroup_Or_Maingroup_Or_MaingroupId",
                table: "Ma_Subgroup");

            migrationBuilder.DropIndex(
                name: "IX_Ma_Subgroup_Or_MaingroupId",
                table: "Ma_Subgroup");

            migrationBuilder.DropColumn(
                name: "Or_MaingroupId",
                table: "Ma_Subgroup");

            migrationBuilder.AddColumn<Guid>(
                name: "maingroup_id",
                table: "Ma_Subgroup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Ma_Subgroup_maingroup_id",
                table: "Ma_Subgroup",
                column: "maingroup_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ma_Subgroup_Or_Maingroup_maingroup_id",
                table: "Ma_Subgroup",
                column: "maingroup_id",
                principalTable: "Or_Maingroup",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
