using BookStoreApi.DTOs;
using BookStoreApi.ViewModels;

namespace BookStoreApi.Interfaces
{
    public interface IGenreService 
    {
        Task<IEnumerable<GenreViewModel>> GetAll();
        Task<GenreViewModel> GetById(int id);
        Task<GenreViewModel> Create(GenreDto dto, string user);
        Task<bool> Update(int id, GenreDto dto, string user);
        Task<bool> Delete(int id, string user);
    }
}
