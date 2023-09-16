using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProniaBackEnd.Database.Base;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Services;
using System.Linq;
using System.Security.AccessControl;

namespace ProniaBackEnd.Database
{
    public class AppDbContext : DbContext
    {
        private readonly UserService _userService;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
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

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
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


            modelBuilder
                .Entity<ProductColor>()
                .HasKey(pc => new { pc.ProductId, pc.ColorId });


            modelBuilder
                .Entity<ProductColor>()
                .HasOne<Product>(pc => pc.Product)
                .WithMany(p => p.ProductColors)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder
               .Entity<ProductColor>()
               .HasOne<Color>(pc => pc.Color)
               .WithMany(c => c.ProductColors)
               .HasForeignKey(pc => pc.ColorId);

            modelBuilder
               .Entity<ProductSize>()
               .HasKey(pc => new { pc.ProductId, pc.SizeId });

            modelBuilder
                .Entity<ProductSize>()
                .HasOne<Product>(pc => pc.Product)
                .WithMany(p => p.ProductSizes)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder
               .Entity<ProductSize>()
               .HasOne<Size>(pc => pc.Size)
               .WithMany(c => c.ProductSizes)
               .HasForeignKey(pc => pc.SizeId);

            modelBuilder
              .Entity<Basket>()
              .ToTable("Baskets")
              .HasOne<User>(u => u.User)
              .WithOne(b => b.Basket);
              


            modelBuilder
                .Entity<User>()
                .ToTable("Users");



            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<EmailMessage> EmailMessage { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<ProductSize> ProductSize { get; set; }
        public DbSet<ProductColor> ProductColor { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<UserEmailToken> UserEmailTokens { get; set; }

        
        

    }
}

