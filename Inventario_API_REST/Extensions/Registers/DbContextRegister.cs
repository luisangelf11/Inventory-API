using Inventario_API_REST.Database;
using Microsoft.EntityFrameworkCore;

namespace Inventario_API_REST.Extensions.Registers
{
    public static class DbContextRegister
    {
        public static void AddDbContextRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InventoryDbContext>(options 
                => options.UseSqlite(configuration.GetConnectionString("DefaultConnection") ?? "Data Source=inventory.db"));
        }
    }
}
