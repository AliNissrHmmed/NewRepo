using ERP;
using ERP.PURCHASES.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Maincategory> Maincategories { get; set; } = null!;
        public DbSet<Rate> Rates { get; set; } = null!;
        public DbSet<Section> Sections { get; set; } = null!;
        public DbSet<Subcategory> Subcategories { get; set; } = null!;
        public DbSet<Or_Order> Orders { get; set; } = null!;
        public DbSet<Or_Suborder> Suborders { get; set; } = null!;
        public DbSet<Attachment> Attachments { get; set; } = null!;
        public DbSet<Ma_Subgroup> Ma_Subgroups { get; set; } = null!;
        public DbSet<Or_Maingroup> Or_Maingroups { get; set; } = null!;
        public DbSet<Users> users { get; set; } = null!;
        public DbSet<Template> templates { get; set; } = null!;
        public DbSet<Ma_subjoin> Ma_subjoins { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Subcategories)
                .WithMany(s => s.Items)
                .HasForeignKey(i => i.Subcategory_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Companies)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.company_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Company>()
                .HasOne(c => c.Maincategories)
                .WithMany(mc => mc.Companies)
                .HasForeignKey(c => c.Maincategory_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Companies)
                .WithMany(c => c.Attachments)
                .HasForeignKey(a => a.company_id)
                .OnDelete(DeleteBehavior.Cascade);

        /*    modelBuilder.Entity<Ma_Subgroup>()
                .HasOne(a => a.maingroups)
                .WithMany(c => c.Ma_Subgroups)
                .HasForeignKey(a => a.maingroup_id) 
                .OnDelete(DeleteBehavior.Cascade);*/

            modelBuilder.Entity<Rate>()
                .HasOne(r => r.Items)
                .WithMany(i => i.Rates)
                .HasForeignKey(r => r.Item_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Subcategory>()
                .HasOne(s => s.Maincategories)
                .WithMany(m => m.Subcategories)
                .HasForeignKey(s => s.Maincategory_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ma_Subgroup>()
                .HasOne(m => m.sections)
                .WithMany(s => s.Ma_Subgroups)
                .HasForeignKey(m => m.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ma_subjoin>()
                .HasOne(m => m.Or_Maingroups)
                .WithMany(o => o.Ma_subjoins)
                .HasForeignKey(m => m.MaingroupId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ma_subjoin>()
                .HasOne(m => m.Ma_Subgroups)
                .WithMany(s => s.Ma_subjoins)
                .HasForeignKey(m => m.SubgroupId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ma_subjoin>()
                .HasOne(m => m.Sections)
                .WithMany(s=>s.Ma_subjoins)
                .HasForeignKey(m => m.section_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ma_subjoin>()
                .HasOne(m => m.User)
                .WithMany(u => u.Ma_subjoins)
                .HasForeignKey(m => m.user_id)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
