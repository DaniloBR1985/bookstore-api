using BookStoreApi.Entities;

namespace BookStoreApi.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task AddAsync(Book book);
        void Update(Book book);
        void Delete(Book book);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(Book book);
        Task<bool> SaveChangesAsync();
    }
}
