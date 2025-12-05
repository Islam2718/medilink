using Microsoft.EntityFrameworkCore;
using Shared.Models.Entities;

namespace Shared.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                // entity.Property(u => u.Role).HasConversion<string>();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}