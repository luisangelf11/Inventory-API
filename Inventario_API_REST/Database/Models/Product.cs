using Microsoft.EntityFrameworkCore;

namespace Inventario_API_REST.Database.Models
{
    public class Product: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }

        [Precision(18, 2)]
        public decimal Cost { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        [Precision(18, 2)]
        public decimal EarningUnit => Price - Cost;
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = null!;
    }
}
