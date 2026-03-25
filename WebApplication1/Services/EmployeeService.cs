using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
    {
        private readonly IEmployeeRepository _repository = repository;

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Employee employee)
        {
            await _repository.AddAsync(employee);
            await _repository.SaveChangesAsync(); // 🔥 commit unique
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var existing = await _repository.GetByIdAsync(employee.Id);

            if (existing == null)
                return false;

            _repository.Update(employee);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return false;

            _repository.Delete(existing);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
