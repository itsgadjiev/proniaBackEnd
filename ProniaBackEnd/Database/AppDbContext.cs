using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

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


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }

    }
}

