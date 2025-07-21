using AutoMapper;
using BookStoreApi.DTOs;
using BookStoreApi.Entities;
using BookStoreApi.Interfaces;
using BookStoreApi.ViewModels;

namespace BookStoreApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository generoRepository, IMapper mapper)
        {
            _genreRepository = generoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreViewModel>> GetAll()
        {
            var genres = await _genreRepository.GetAllAsync();
            var viewModels = _mapper.Map<IEnumerable<GenreViewModel>>(genres);
            return viewModels;
        }

        public async Task<GenreViewModel> GetById(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
                return null;

            return _mapper.Map<GenreViewModel>(genre);
        }

        public async Task<GenreViewModel> Create(GenreDto dto, string user)
        {
            if (await _genreRepository.ExistsAsync(dto.Name))
                throw new Exception("Já existe o nome desse genero.");
            else if (string.IsNullOrEmpty(dto.Name))
                throw new Exception("O nome do genero não pode ser vazio.");

            var genre = new Genre
            {
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = user
            };

            await _genreRepository.AddAsync(genre);
            await _genreRepository.SaveChangesAsync();

            return _mapper.Map<GenreViewModel>(genre);
        }

        public async Task<bool> Update(int id, GenreDto dto, string user)
        {
            if (await _genreRepository.ExistsAsync(dto.Name))
                throw new Exception("Já existe o nome desse genero.");
            else if (string.IsNullOrEmpty(dto.Name))
                throw new Exception("O nome do genero não pode ser vazio.");

            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
                return false;

            genre.Name = dto.Name;
            genre.UpdatedAt = DateTime.UtcNow;
            genre.UpdatedBy = user;

            _genreRepository.Update(genre);
            return await _genreRepository.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id, string user)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
                return false;

            // Atualiza dados de auditoria antes de deletar (opcional)
            genre.UpdatedAt = DateTime.UtcNow;
            genre.UpdatedBy = user;

            _genreRepository.Delete(genre);
            return await _genreRepository.SaveChangesAsync();
        }
    }
}
