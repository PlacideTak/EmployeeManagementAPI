using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; }

        // Navigation property pour les employés associés
        public List<Employee>? Employees { get; set; } = [];
    }
}