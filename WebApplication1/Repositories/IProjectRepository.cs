using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task AddAsync(Project project);
        void Update(Project project);
        void Delete(Project project);
        Task SaveChangesAsync();
    }
}
