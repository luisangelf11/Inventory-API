using Inventario_API_REST.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventario_API_REST.Database
{
    public static class DbInitializer
    {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

            await context.Database.MigrateAsync();

            if (!await context.Roles.AnyAsync())
            {
                var roles = new List<Role> {
                    new Role { Id = 1, Name = RolesName.Admin},
                    new Role { Id = 2, Name = RolesName.Seller},
                };

                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }

            if (!await context.Permissions.AnyAsync())
            {
                var permissions = new List<Permission>
                {
                    new Permission {Name = Permissions.READ},
                    new Permission {Name = Permissions.CREATE},
                    new Permission {Name = Permissions.DELETE},
                    new Permission {Name = Permissions.UPDATE},
                };
                context.Permissions.AddRange(permissions);
                await context.SaveChangesAsync();
            }

            if (!await context.Users.AnyAsync(u => u.Username == "admin"))
            {
                var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == RolesName.Admin);

                if (adminRole != null)
                {
                    var adminUser = new User
                    {
                        Username = "admin",
                        Password = BCrypt.Net.BCrypt.HashPassword("Admin123*"),
                        RoleId = adminRole.Id,
                        CreatedAt = DateTime.UtcNow,
                        Permissions = $"{Permissions.READ};{Permissions.CREATE};{Permissions.DELETE};{Permissions.UPDATE}"
                    };

                    context.Users.Add(adminUser);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
