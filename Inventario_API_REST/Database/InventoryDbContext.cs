using Inventario_API_REST.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventario_API_REST.Database
{
    public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ent =>
            {
                ent.HasIndex(u => u.Username).IsUnique();
                ent.HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId);
            });

            modelBuilder.Entity<Product>(ent =>
            {
                ent.Property(p => p.Cost).HasConversion<double>();
                ent.Property(p => p.Price).HasConversion<double>();
                ent.HasOne(p => p.CreatedBy)
                    .WithMany()
                    .HasForeignKey(u => u.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>().Ignore(p => p.EarningUnit);
        }
    }
}
