using BookStoreApi.Entities;
using BookStoreApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(l => l.Author)
                .Include(l => l.Genre)
                .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(l => l.Author)
                .Include(l => l.Genre)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddAsync(Book livro)
        {
            await _context.Books.AddAsync(livro);
        }

        public void Update(Book livro)
        {
            _context.Books.Update(livro);
        }

        public void Delete(Book livro)
        {
            _context.Books.Remove(livro);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }
        public async Task<bool> ExistsAsync(Book book)
        {
            return await _context.Books.AnyAsync(b=> b.Title == book.Title && b.AuthorId == book.AuthorId && b.GenreId == book.GenreId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
