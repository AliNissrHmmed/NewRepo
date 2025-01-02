using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPPURCHASES.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration343jkjgfhgfhfghf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ma_Subgroup_Or_Maingroup_code",
                table: "Ma_Subgroup");

            migrationBuilder.DropIndex(
                name: "IX_Ma_Subgroup_code",
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

            migrationBuilder.CreateIndex(
                name: "IX_Ma_Subgroup_code",
                table: "Ma_Subgroup",
                column: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_Ma_Subgroup_Or_Maingroup_code",
                table: "Ma_Subgroup",
                column: "code",
                principalTable: "Or_Maingroup",
                principalColumn: "id");
        }
    }
}
