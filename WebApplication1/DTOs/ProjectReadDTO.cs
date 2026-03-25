namespace WebApplication1.DTOs
{
    public class ProjectReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Optionnel : lister les employés associés
        public IEnumerable<EmployeeReadDTO>? Employees { get; set; } = [];
    }
}
