using BookStoreApi.Entities;
using BookStoreApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors
                .ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Author autor)
        {
            await _context.Authors.AddAsync(autor);
        }

        public void Update(Author autor)
        {
            _context.Authors.Update(autor);
        }

        public void Delete(Author autor)
        {
            _context.Authors.Remove(autor);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Authors.AnyAsync(a => a.Name.ToUpper() == name.ToUpper());
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
