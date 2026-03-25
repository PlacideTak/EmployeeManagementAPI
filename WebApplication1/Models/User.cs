using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string ExternalId { get; set; } = string.Empty; // Id venant du token

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
    }
}
