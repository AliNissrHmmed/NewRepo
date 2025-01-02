﻿// <auto-generated />
using System;
using ERP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ERPPURCHASES.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241230141205_update_subjoin")]
    partial class update_subjoin
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ERP.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<Guid>("company_id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("company_id");

                    b.Property<bool>("state")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("url");

                    b.Property<Guid?>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("company_id");

                    b.ToTable("Co_Attachment");
                });

            modelBuilder.Entity("ERP.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<Guid>("Maincategory_id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Maincategory_id");

                    b.Property<Guid>("Organization_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Organization_id");

                    b.Property<bool>("State")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.Property<string>("phone2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone2");

                    b.Property<Guid>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("Maincategory_id");

                    b.ToTable("Pu_Companies");
                });

            modelBuilder.Entity("ERP.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<bool>("State")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<Guid>("Subcategory_id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Subcategory_id");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("company_id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("company_id");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<Guid>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("Subcategory_id");

                    b.HasIndex("company_id");

                    b.ToTable("Co_Items");
                });

            modelBuilder.Entity("ERP.Ma_Subgroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<Guid>("SectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("State")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("code");

                    b.Property<string>("itemtype")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("itemtype");

                    b.Property<string>("note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("note");

                    b.Property<bool>("suptreegroup")
                        .HasColumnType("bit")
                        .HasColumnName("suptreegroup");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.HasIndex("user_id");

                    b.ToTable("Ma_Subgroup");
                });

            modelBuilder.Entity("ERP.Ma_subjoin", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MaingroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<Guid>("SubgroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("code")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("section_id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("MaingroupId");

                    b.HasIndex("SubgroupId");

                    b.HasIndex("section_id");

                    b.HasIndex("user_id");

                    b.ToTable("Ma_subjoin");
                });

            modelBuilder.Entity("ERP.Maincategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<Guid?>("Organization_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Organization_id");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<bool>("state")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<Guid?>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("Pu_Maincategory");
                });

            modelBuilder.Entity("ERP.Or_Maingroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<Guid>("Organization_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Organization_id");

                    b.Property<bool>("State")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("code");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("user_id");

                    b.ToTable("Or_Maingroup");
                });

            modelBuilder.Entity("ERP.Or_Order", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.ToTable("Or_order");
                });

            modelBuilder.Entity("ERP.Or_Suborder", b =>
                {
                    b.Property<Guid>("UniqueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Item_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Item_id");

                    b.Property<Guid>("ItemsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Ordersid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UniqueID");

                    b.HasIndex("ItemsId");

                    b.HasIndex("Ordersid");

                    b.ToTable("Or_Suborder");
                });

            modelBuilder.Entity("ERP.PURCHASES.Models.Su_members", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<Guid>("Templatesid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("code")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("code");

                    b.Property<bool>("state")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<Guid>("temp_id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Templatesid");

                    b.HasIndex("user_id");

                    b.ToTable("Su_members");
                });

            modelBuilder.Entity("ERP.PURCHASES.Models.Template", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdAt");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nickname");

                    b.Property<bool>("state")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedAt");

                    b.HasKey("id");

                    b.ToTable("templates");
                });

            modelBuilder.Entity("ERP.PURCHASES.Models.Users", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdAt");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("serial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("serial");

                    b.Property<bool>("state")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.HasKey("id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ERP.Rate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<Guid>("Item_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Item_id");

                    b.Property<bool>("State")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.Property<string>("note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("range")
                        .HasColumnType("int");

                    b.Property<Guid>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("Item_id");

                    b.ToTable("It_Rate");
                });

            modelBuilder.Entity("ERP.Section", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<Guid>("Organization_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Organization_id");

                    b.Property<bool>("State")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("serial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("Or_Sections");
                });

            modelBuilder.Entity("ERP.Subcategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<Guid>("Maincategory_id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Maincategory_id");

                    b.Property<Guid>("Organization_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Organization_id");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<bool>("state")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<Guid>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("Maincategory_id");

                    b.ToTable("Pu_Subcategory");
                });

            modelBuilder.Entity("Ma_SubgroupOr_Maingroup", b =>
                {
                    b.Property<Guid>("Ma_SubgroupsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("or_MaingroupsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Ma_SubgroupsId", "or_MaingroupsId");

                    b.HasIndex("or_MaingroupsId");

                    b.ToTable("Ma_SubgroupOr_Maingroup");
                });

            modelBuilder.Entity("ERP.Attachment", b =>
                {
                    b.HasOne("ERP.Company", "Companies")
                        .WithMany("Attachments")
                        .HasForeignKey("company_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Companies");
                });

            modelBuilder.Entity("ERP.Company", b =>
                {
                    b.HasOne("ERP.Maincategory", "Maincategories")
                        .WithMany("Companies")
                        .HasForeignKey("Maincategory_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Maincategories");
                });

            modelBuilder.Entity("ERP.Item", b =>
                {
                    b.HasOne("ERP.Subcategory", "Subcategories")
                        .WithMany("Items")
                        .HasForeignKey("Subcategory_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERP.Company", "Companies")
                        .WithMany("Items")
                        .HasForeignKey("company_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Companies");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("ERP.Ma_Subgroup", b =>
                {
                    b.HasOne("ERP.Section", "sections")
                        .WithMany("Ma_Subgroups")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ERP.PURCHASES.Models.Users", "User")
                        .WithMany("Ma_Subgroups")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("sections");
                });

            modelBuilder.Entity("ERP.Ma_subjoin", b =>
                {
                    b.HasOne("ERP.Or_Maingroup", "Or_Maingroups")
                        .WithMany("Ma_subjoins")
                        .HasForeignKey("MaingroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ERP.Ma_Subgroup", "Ma_Subgroups")
                        .WithMany("Ma_subjoins")
                        .HasForeignKey("SubgroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ERP.Section", "Sections")
                        .WithMany("Ma_subjoins")
                        .HasForeignKey("section_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ERP.PURCHASES.Models.Users", "User")
                        .WithMany("Ma_subjoins")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ma_Subgroups");

                    b.Navigation("Or_Maingroups");

                    b.Navigation("Sections");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ERP.Or_Maingroup", b =>
                {
                    b.HasOne("ERP.PURCHASES.Models.Users", "User")
                        .WithMany("Or_Maingroups")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ERP.Or_Suborder", b =>
                {
                    b.HasOne("ERP.Item", "Items")
                        .WithMany("OrSuborderes")
                        .HasForeignKey("ItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERP.Or_Order", "Orders")
                        .WithMany("Suborders")
                        .HasForeignKey("Ordersid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Items");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ERP.PURCHASES.Models.Su_members", b =>
                {
                    b.HasOne("ERP.PURCHASES.Models.Template", "Templates")
                        .WithMany("su_Members")
                        .HasForeignKey("Templatesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERP.PURCHASES.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Templates");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ERP.Rate", b =>
                {
                    b.HasOne("ERP.Item", "Items")
                        .WithMany("Rates")
                        .HasForeignKey("Item_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Items");
                });

            modelBuilder.Entity("ERP.Subcategory", b =>
                {
                    b.HasOne("ERP.Company", null)
                        .WithMany("Subcategories")
                        .HasForeignKey("CompanyId");

                    b.HasOne("ERP.Maincategory", "Maincategories")
                        .WithMany("Subcategories")
                        .HasForeignKey("Maincategory_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Maincategories");
                });

            modelBuilder.Entity("Ma_SubgroupOr_Maingroup", b =>
                {
                    b.HasOne("ERP.Ma_Subgroup", null)
                        .WithMany()
                        .HasForeignKey("Ma_SubgroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERP.Or_Maingroup", null)
                        .WithMany()
                        .HasForeignKey("or_MaingroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ERP.Company", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Items");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("ERP.Item", b =>
                {
                    b.Navigation("OrSuborderes");

                    b.Navigation("Rates");
                });

            modelBuilder.Entity("ERP.Ma_Subgroup", b =>
                {
                    b.Navigation("Ma_subjoins");
                });

            modelBuilder.Entity("ERP.Maincategory", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("ERP.Or_Maingroup", b =>
                {
                    b.Navigation("Ma_subjoins");
                });

            modelBuilder.Entity("ERP.Or_Order", b =>
                {
                    b.Navigation("Suborders");
                });

            modelBuilder.Entity("ERP.PURCHASES.Models.Template", b =>
                {
                    b.Navigation("su_Members");
                });

            modelBuilder.Entity("ERP.PURCHASES.Models.Users", b =>
                {
                    b.Navigation("Ma_Subgroups");

                    b.Navigation("Ma_subjoins");

                    b.Navigation("Or_Maingroups");
                });

            modelBuilder.Entity("ERP.Section", b =>
                {
                    b.Navigation("Ma_Subgroups");

                    b.Navigation("Ma_subjoins");
                });

            modelBuilder.Entity("ERP.Subcategory", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
