namespace WebApplication1.DTOs
{
    public class EmployeeReadDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<ProjectReadDTO>? Projects { get; set; } = [];
    }
}
