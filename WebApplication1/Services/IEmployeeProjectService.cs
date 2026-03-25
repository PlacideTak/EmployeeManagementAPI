namespace WebApplication1.Services
{
    public interface IEmployeeProjectService
    {
        Task<bool> AssignEmployeeToProjectAsync(int employeeId, int projectId);
        Task<bool> RemoveEmployeeFromProjectAsync(int employeeId, int projectId);
    }
}
