using AutoMapper;
using BookStoreApi.DTOs;
using BookStoreApi.Entities;
using BookStoreApi.Interfaces;
using BookStoreApi.ViewModels;

namespace BookStoreApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _autorRepository;
        private readonly IGenreRepository _generoRepository;
        private readonly IMapper _mapper;

        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository autorRepository,
            IGenreRepository generoRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _autorRepository = autorRepository;
            _generoRepository = generoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookViewModel>> GetAll()
        {
            var livros = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookViewModel>>(livros);
        }

        public async Task<BookViewModel> GetById(int id)
        {
            var livro = await _bookRepository.GetByIdAsync(id);
            if (livro == null) 
                return null;
            return _mapper.Map<BookViewModel>(livro);
        }

        public async Task<BookViewModel> Create(BookDto dto, string user)
        {
            var autorExists = await _autorRepository.ExistsAsync(dto.AuthorId);
            var generoExists = await _generoRepository.ExistsAsync(dto.GenreId);
            if (await _bookRepository.ExistsAsync(_mapper.Map<Book>(dto)))
                throw new Exception("Já existe titulo de livro com esse autor e esse genero.");

            if (!autorExists || !generoExists)
                throw new Exception("Não existe autor e/ou não existe esse genero.");
            else if (string.IsNullOrEmpty(dto.Title))
                throw new Exception("O nome do titulo não pode ser vazio.");

            var book = new Book
            {
                Title = dto.Title,
                AuthorId = dto.AuthorId,
                GenreId = dto.GenreId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = user
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            return _mapper.Map<BookViewModel>(book);
        }

        public async Task<bool> Update(int id, BookDto dto, string user)
        {
            var livro = await _bookRepository.GetByIdAsync(id);
            if (livro == null) return false;

            var autorExists = await _autorRepository.ExistsAsync(dto.AuthorId);
            var generoExists = await _generoRepository.ExistsAsync(dto.GenreId);
            if (await _bookRepository.ExistsAsync(_mapper.Map<Book>(dto)))
                throw new Exception("Já existe titulo de livro com esse autor e esse genero.");

            if (!autorExists || !generoExists)
                throw new Exception("Não existe autor e/ou não existe esse genero.");
            else if (string.IsNullOrEmpty(dto.Title))
                throw new Exception("O nome do titulo não pode ser vazio.");

            livro.Title = dto.Title;
            livro.AuthorId = dto.AuthorId;
            livro.GenreId = dto.GenreId;
            livro.UpdatedAt = DateTime.UtcNow;
            livro.UpdatedBy = user;

            _bookRepository.Update(livro);
            return await _bookRepository.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id, string user)
        {
            var livro = await _bookRepository.GetByIdAsync(id);
            if (livro == null) return false;

            livro.UpdatedAt = DateTime.UtcNow;
            livro.UpdatedBy = user;

            _bookRepository.Delete(livro);
            return await _bookRepository.SaveChangesAsync();
        }
    }

}
