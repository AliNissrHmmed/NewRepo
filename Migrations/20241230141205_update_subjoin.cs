using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPPURCHASES.Migrations
{
    /// <inheritdoc />
    public partial class update_subjoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Or_order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Or_order", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Or_Sections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    serial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Or_Sections", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pu_Maincategory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    state = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pu_Maincategory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "templates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_templates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    serial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pu_Companies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maincategory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pu_Companies", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pu_Companies_Pu_Maincategory_Maincategory_id",
                        column: x => x.Maincategory_id,
                        principalTable: "Pu_Maincategory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ma_Subgroup",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    itemtype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    suptreegroup = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ma_Subgroup", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ma_Subgroup_Or_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Or_Sections",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Ma_Subgroup_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Or_Maingroup",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Or_Maingroup", x => x.id);
                    table.ForeignKey(
                        name: "FK_Or_Maingroup_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Su_members",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    temp_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Templatesid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Su_members", x => x.id);
                    table.ForeignKey(
                        name: "FK_Su_members_templates_Templatesid",
                        column: x => x.Templatesid,
                        principalTable: "templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Su_members_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Co_Attachment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    state = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Co_Attachment", x => x.id);
                    table.ForeignKey(
                        name: "FK_Co_Attachment_Pu_Companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Pu_Companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pu_Subcategory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Maincategory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Organization_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pu_Subcategory", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pu_Subcategory_Pu_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Pu_Companies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Pu_Subcategory_Pu_Maincategory_Maincategory_id",
                        column: x => x.Maincategory_id,
                        principalTable: "Pu_Maincategory",
                        principalColumn: "id");
                });

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

            migrationBuilder.CreateTable(
                name: "Ma_subjoin",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaingroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubgroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    section_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ma_subjoin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ma_subjoin_Ma_Subgroup_SubgroupId",
                        column: x => x.SubgroupId,
                        principalTable: "Ma_Subgroup",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Ma_subjoin_Or_Maingroup_MaingroupId",
                        column: x => x.MaingroupId,
                        principalTable: "Or_Maingroup",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Ma_subjoin_Or_Sections_section_id",
                        column: x => x.section_id,
                        principalTable: "Or_Sections",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Ma_subjoin_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Co_Items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subcategory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Co_Items", x => x.id);
                    table.ForeignKey(
                        name: "FK_Co_Items_Pu_Companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Pu_Companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Co_Items_Pu_Subcategory_Subcategory_id",
                        column: x => x.Subcategory_id,
                        principalTable: "Pu_Subcategory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "It_Rate",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    range = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_It_Rate", x => x.id);
                    table.ForeignKey(
                        name: "FK_It_Rate_Co_Items_Item_id",
                        column: x => x.Item_id,
                        principalTable: "Co_Items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Or_Suborder",
                columns: table => new
                {
                    UniqueID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    Ordersid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Or_Suborder", x => x.UniqueID);
                    table.ForeignKey(
                        name: "FK_Or_Suborder_Co_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Co_Items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Or_Suborder_Or_order_Ordersid",
                        column: x => x.Ordersid,
                        principalTable: "Or_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Co_Attachment_company_id",
                table: "Co_Attachment",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Co_Items_company_id",
                table: "Co_Items",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Co_Items_Subcategory_id",
                table: "Co_Items",
                column: "Subcategory_id");

            migrationBuilder.CreateIndex(
                name: "IX_It_Rate_Item_id",
                table: "It_Rate",
                column: "Item_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ma_Subgroup_SectionId",
                table: "Ma_Subgroup",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ma_Subgroup_user_id",
                table: "Ma_Subgroup",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ma_SubgroupOr_Maingroup_or_MaingroupsId",
                table: "Ma_SubgroupOr_Maingroup",
                column: "or_MaingroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ma_subjoin_MaingroupId",
                table: "Ma_subjoin",
                column: "MaingroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Ma_subjoin_section_id",
                table: "Ma_subjoin",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ma_subjoin_SubgroupId",
                table: "Ma_subjoin",
                column: "SubgroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Ma_subjoin_user_id",
                table: "Ma_subjoin",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Or_Maingroup_user_id",
                table: "Or_Maingroup",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Or_Suborder_ItemsId",
                table: "Or_Suborder",
                column: "ItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_Or_Suborder_Ordersid",
                table: "Or_Suborder",
                column: "Ordersid");

            migrationBuilder.CreateIndex(
                name: "IX_Pu_Companies_Maincategory_id",
                table: "Pu_Companies",
                column: "Maincategory_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pu_Subcategory_CompanyId",
                table: "Pu_Subcategory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Pu_Subcategory_Maincategory_id",
                table: "Pu_Subcategory",
                column: "Maincategory_id");

            migrationBuilder.CreateIndex(
                name: "IX_Su_members_Templatesid",
                table: "Su_members",
                column: "Templatesid");

            migrationBuilder.CreateIndex(
                name: "IX_Su_members_user_id",
                table: "Su_members",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Co_Attachment");

            migrationBuilder.DropTable(
                name: "It_Rate");

            migrationBuilder.DropTable(
                name: "Ma_SubgroupOr_Maingroup");

            migrationBuilder.DropTable(
                name: "Ma_subjoin");

            migrationBuilder.DropTable(
                name: "Or_Suborder");

            migrationBuilder.DropTable(
                name: "Su_members");

            migrationBuilder.DropTable(
                name: "Ma_Subgroup");

            migrationBuilder.DropTable(
                name: "Or_Maingroup");

            migrationBuilder.DropTable(
                name: "Co_Items");

            migrationBuilder.DropTable(
                name: "Or_order");

            migrationBuilder.DropTable(
                name: "templates");

            migrationBuilder.DropTable(
                name: "Or_Sections");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "Pu_Subcategory");

            migrationBuilder.DropTable(
                name: "Pu_Companies");

            migrationBuilder.DropTable(
                name: "Pu_Maincategory");
        }
    }
}
