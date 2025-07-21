using BookStoreApi.DTOs;
using BookStoreApi.ViewModels;

namespace BookStoreApi.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorViewModel>> GetAll();

        Task<AuthorViewModel> GetById(int id);

        Task<AuthorViewModel> Create(AuthorDto dto, string user);

        Task<bool> Update(int id, AuthorDto dto, string user);

        Task<bool> Delete(int id, string user);
    }
}
