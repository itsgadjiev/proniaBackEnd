using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Interfaces;

namespace ProniaBackEnd.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IAuditable)
                {
                    var auditable = (IAuditable)entry.Entity;

                    if (entry.State == EntityState.Added)
                    {
                        auditable.UpdatedOn = DateTime.UtcNow;
                        auditable.CreatedOn = DateTime.UtcNow;
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        auditable.UpdatedOn = DateTime.UtcNow;
                    }
                }
            }

            return base.SaveChanges();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId })
                .HasName("ProductCategories");

            modelBuilder
                .Entity<ProductCategory>()
                .HasOne<Product>(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder
               .Entity<ProductCategory>()
               .HasOne<Category>(pc => pc.Category)
               .WithMany(c => c.ProductCategories)
               .HasForeignKey(pc => pc.CategoryId);

            modelBuilder
                .Entity<EmailMessage>();

            modelBuilder.Entity<BasketItem>()
                .HasOne(bi => bi.Product)
                .WithMany()
                .HasForeignKey(bi => bi.ProductId);
                

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<EmailMessage> EmailMessage { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }


    }
}

