using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> GetByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<User?> GetByExternalIdAsync(string externalId) =>
            await _repository.GetByExternalIdAsync(externalId);

        public async Task<User?> GetByEmailAsync(string email) =>
            await _repository.GetByEmailAsync(email);

        public async Task<User> CreateAsync(User user)
        {
            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _repository.Update(user);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _repository.Delete(user);
            await _repository.SaveChangesAsync();
        }
    }
}
