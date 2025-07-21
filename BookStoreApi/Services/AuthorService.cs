using AutoMapper;
using BookStoreApi.DTOs;
using BookStoreApi.Entities;
using BookStoreApi.Interfaces;
using BookStoreApi.ViewModels;

namespace BookStoreApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository autorRepository, IMapper mapper)
        {
            _authorRepository = autorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorViewModel>> GetAll()
        {
            var authors = await _authorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorViewModel>>(authors);
        }

        public async Task<AuthorViewModel> GetById(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return null;
            return _mapper.Map<AuthorViewModel>(author);
        }

        public async Task<AuthorViewModel> Create(AuthorDto dto, string user)
        {
            if (await _authorRepository.ExistsAsync(dto.Name))
                throw new Exception("Já existe o nome desse autor.");
            else if (string.IsNullOrEmpty(dto.Name))
                    throw new Exception("O nome desse autor não pode ser vazio.");

            var author = new Author
            {
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = user
            };

            await _authorRepository.AddAsync(author);
            await _authorRepository.SaveChangesAsync();

            return _mapper.Map<AuthorViewModel>(author);
        }

        public async Task<bool> Update(int id, AuthorDto dto, string user)
        {
            if (await _authorRepository.ExistsAsync(dto.Name))
                throw new Exception("Já existe o nome desse autor.");
            else if (string.IsNullOrEmpty(dto.Name))
                throw new Exception("O nome desse autor não pode ser vazio.");

            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return false;

            author.Name = dto.Name;
            author.UpdatedAt = DateTime.UtcNow;
            author.UpdatedBy = user;

            _authorRepository.Update(author);
            return await _authorRepository.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id, string user)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return false;

            author.UpdatedAt = DateTime.UtcNow;
            author.UpdatedBy = user;

            _authorRepository.Delete(author);
            return await _authorRepository.SaveChangesAsync();
        }
    }
}
