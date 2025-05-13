using AspWebApps.Models;
using Microsoft.EntityFrameworkCore;

namespace AspWebApps.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fruits", DisplayOrder = 1},
                new Category { Id = 2, Name = "Vegetables", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Meat & Fish", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Cooking Ingrediants", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Dairy", DisplayOrder = 5 }
            );
        }
    }
}
