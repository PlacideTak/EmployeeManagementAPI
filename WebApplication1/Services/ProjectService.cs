using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly AppDbContext _context; // nécessaire pour charger les Employees

        public ProjectService(IProjectRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddProjectAsync(Project project, IEnumerable<int>? employeeIds = null)
        {
            // Charger les employés depuis la DB si EmployeeIds fournis
            if (employeeIds != null && employeeIds.Any())
            {
                project.Employees = await _context.Employees
                    .Where(e => employeeIds.Contains(e.Id))
                    .ToListAsync();
            }

            await _repository.AddAsync(project);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project, IEnumerable<int>? employeeIds = null)
        {
            // Mettre à jour les employés associés si EmployeeIds fournis
            if (employeeIds != null)
            {
                var employees = await _context.Employees
                    .Where(e => employeeIds.Contains(e.Id))
                    .ToListAsync();

                project.Employees.Clear();
                project.Employees.AddRange(employees);
            }

            _repository.Update(project);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(Project project)
        {
            _repository.Delete(project);
            await _repository.SaveChangesAsync();
        }
    }
}
