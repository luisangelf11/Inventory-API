using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Inventario_API_REST.Database.Models
{
    public class User: BaseEntity
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public string Permissions { get; set; } = string.Empty;
        public Role Role { get; set; } = null!;
    }
}
