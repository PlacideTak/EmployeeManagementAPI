using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project?> GetProjectByIdAsync(int id);
        Task AddProjectAsync(Project project, IEnumerable<int>? employeeIds);
        Task UpdateProjectAsync(Project project, IEnumerable<int>? employeeIds);
        Task DeleteProjectAsync(Project project);
    }
}
