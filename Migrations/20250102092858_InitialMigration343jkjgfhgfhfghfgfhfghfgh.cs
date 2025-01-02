using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPPURCHASES.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration343jkjgfhgfhfghfgfhfghfgh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "code",
                table: "Or_Maingroup",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "code",
                table: "Ma_Subgroup",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "Or_Maingroup",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "Ma_Subgroup",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
    }
}
