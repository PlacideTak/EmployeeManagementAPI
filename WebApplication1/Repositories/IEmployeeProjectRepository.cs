namespace WebApplication1.Repositories
{
    public interface IEmployeeProjectRepository
    {
        Task<bool> AssignEmployeeToProjectAsync(int employeeId, int projectId);
        Task<bool> RemoveEmployeeFromProjectAsync(int employeeId, int projectId);
    }
}
