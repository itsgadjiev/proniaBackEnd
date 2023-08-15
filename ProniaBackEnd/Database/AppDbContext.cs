using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.Database
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=ProniaDB;User Id=postgres;Password=admin;");

            base.OnConfiguring(optionsBuilder);

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }

    }
}

