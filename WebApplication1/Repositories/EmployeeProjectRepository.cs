using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class EmployeeProjectRepository : IEmployeeProjectRepository
    {
        private readonly AppDbContext _context;

        public EmployeeProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AssignEmployeeToProjectAsync(int employeeId, int projectId)
        {
            var employee = await _context.Employees
                .Include(e => e.Projects)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return false;

            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
                return false;

            // éviter duplication
            if (employee.Projects.Any(p => p.Id == projectId))
                return false;

            employee.Projects.Add(project);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveEmployeeFromProjectAsync(int employeeId, int projectId)
        {
            var employee = await _context.Employees
                .Include(e => e.Projects)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return false;

            var project = employee.Projects.FirstOrDefault(p => p.Id == projectId);

            if (project == null)
                return false;

            employee.Projects.Remove(project);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
