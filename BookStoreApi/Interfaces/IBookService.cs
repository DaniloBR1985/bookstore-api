using BookStoreApi.DTOs;
using BookStoreApi.ViewModels;

namespace BookStoreApi.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAll();
        Task<BookViewModel> GetById(int id);
        Task<BookViewModel> Create(BookDto dto, string user);
        Task<bool> Update(int id, BookDto dto, string user);
        Task<bool> Delete(int id, string user);
    }
}
