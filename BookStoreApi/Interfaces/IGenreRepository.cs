using BookStoreApi.Entities;

namespace BookStoreApi.Interfaces
{
    public interface IGenreRepository 
    { 
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetByIdAsync(int id);
        Task AddAsync(Genre genre);
        void Update(Genre genre);
        void Delete(Genre genre);
        Task<bool> SaveChangesAsync();
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string name);
    }
}
