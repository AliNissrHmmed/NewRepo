using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPPURCHASES.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ma_Subgroup_Or_Sections_SectionId",
                table: "Ma_Subgroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Ma_Subgroup_users_user_id",
                table: "Ma_Subgroup");

            migrationBuilder.AddForeignKey(
                name: "FK_Ma_Subgroup_Or_Sections_SectionId",
                table: "Ma_Subgroup",
                column: "SectionId",
                principalTable: "Or_Sections",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ma_Subgroup_users_user_id",
                table: "Ma_Subgroup",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ma_Subgroup_Or_Sections_SectionId",
                table: "Ma_Subgroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Ma_Subgroup_users_user_id",
                table: "Ma_Subgroup");

            migrationBuilder.AddForeignKey(
                name: "FK_Ma_Subgroup_Or_Sections_SectionId",
                table: "Ma_Subgroup",
                column: "SectionId",
                principalTable: "Or_Sections",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ma_Subgroup_users_user_id",
                table: "Ma_Subgroup",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
