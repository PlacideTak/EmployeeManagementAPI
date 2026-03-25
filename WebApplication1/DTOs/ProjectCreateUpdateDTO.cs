using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class ProjectCreateUpdateDTO
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }

        // Optionnel : IDs des employés associés
        public IEnumerable<int> EmployeeIds { get; set; } = [];
    }
}
