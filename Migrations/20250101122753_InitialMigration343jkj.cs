using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPPURCHASES.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration343jkj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ma_Subgroup_Or_Maingroup_code",
                table: "Ma_Subgroup");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Or_Maingroup_code",
                table: "Or_Maingroup");

            migrationBuilder.AlterColumn<Guid>(
                name: "code",
                table: "Or_Maingroup",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "code",
                table: "Ma_Subgroup",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Ma_Subgroup_Or_Maingroup_code",
                table: "Ma_Subgroup",
                column: "code",
                principalTable: "Or_Maingroup",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ma_Subgroup_Or_Maingroup_code",
                table: "Ma_Subgroup");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "Or_Maingroup",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "Ma_Subgroup",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Or_Maingroup_code",
                table: "Or_Maingroup",
                column: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_Ma_Subgroup_Or_Maingroup_code",
                table: "Ma_Subgroup",
                column: "code",
                principalTable: "Or_Maingroup",
                principalColumn: "code");
        }
    }
}
