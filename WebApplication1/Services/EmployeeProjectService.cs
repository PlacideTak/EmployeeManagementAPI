using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class EmployeeProjectService : IEmployeeProjectService
    {
        private readonly IEmployeeProjectRepository _repository;

        public EmployeeProjectService(IEmployeeProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AssignEmployeeToProjectAsync(int employeeId, int projectId)
        {
            return await _repository.AssignEmployeeToProjectAsync(employeeId, projectId);
        }

        public async Task<bool> RemoveEmployeeFromProjectAsync(int employeeId, int projectId)
        {
            return await _repository.RemoveEmployeeFromProjectAsync(employeeId, projectId);
        }
    }
}
