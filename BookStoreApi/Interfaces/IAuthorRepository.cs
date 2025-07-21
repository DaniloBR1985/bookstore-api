using BookStoreApi.Entities;

namespace BookStoreApi.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();

        Task<Author> GetByIdAsync(int id);

        Task AddAsync(Author autor);

        void Update(Author autor);

        void Delete(Author autor);

        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string name);

        Task<bool> SaveChangesAsync();
    }
}
