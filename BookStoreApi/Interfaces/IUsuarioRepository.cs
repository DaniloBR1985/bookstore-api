using BookStoreApi.Entities;

namespace BookStoreApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUserAsync(string username);
        Task AddAsync(User usuario);
        Task<bool> ExistsAsync(string username);
    }
}
