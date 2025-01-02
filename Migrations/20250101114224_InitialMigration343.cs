using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPPURCHASES.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration343 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ma_Subgroup_users_user_id",
                table: "Ma_Subgroup");

            migrationBuilder.DropTable(
                name: "Ma_SubgroupOr_Maingroup");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "Or_Maingroup",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "Ma_Subgroup",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Or_Maingroup_code",
                table: "Or_Maingroup",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_Ma_Subgroup_code",
                table: "Ma_Subgroup",
                column: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_Ma_Subgroup_Or_Maingroup_code",
                table: "Ma_Subgroup",
                column: "code",
                principalTable: "Or_Maingroup",
                principalColumn: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_Ma_Subgroup_users_user_id",
                table: "Ma_Subgroup",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ma_Subgroup_Or_Maingroup_code",
                table: "Ma_Subgroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Ma_Subgroup_users_user_id",
                table: "Ma_Subgroup");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Or_Maingroup_code",
                table: "Or_Maingroup");

            migrationBuilder.DropIndex(
                name: "IX_Ma_Subgroup_code",
                table: "Ma_Subgroup");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "Or_Maingroup",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "Ma_Subgroup",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Ma_SubgroupOr_Maingroup",
                columns: table => new
                {
                    Ma_SubgroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    or_MaingroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ma_SubgroupOr_Maingroup", x => new { x.Ma_SubgroupsId, x.or_MaingroupsId });
                    table.ForeignKey(
                        name: "FK_Ma_SubgroupOr_Maingroup_Ma_Subgroup_Ma_SubgroupsId",
                        column: x => x.Ma_SubgroupsId,
                        principalTable: "Ma_Subgroup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ma_SubgroupOr_Maingroup_Or_Maingroup_or_MaingroupsId",
                        column: x => x.or_MaingroupsId,
                        principalTable: "Or_Maingroup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ma_SubgroupOr_Maingroup_or_MaingroupsId",
                table: "Ma_SubgroupOr_Maingroup",
                column: "or_MaingroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ma_Subgroup_users_user_id",
                table: "Ma_Subgroup",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
