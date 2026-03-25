namespace WebApplication1.DTOs
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
    }
}
