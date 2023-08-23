using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}

