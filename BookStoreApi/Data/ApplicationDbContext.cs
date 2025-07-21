using BookStoreApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BookStoreApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions opt) : base(opt) { }

        public override int SaveChanges()
        {
            // intercept SaveChanges e grava AuditLogs (quem/quando)
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Genre>()
                .HasMany(g => g.Books)
                .WithOne(l => l.Genre)
                .HasForeignKey(l => l.GenreId);

            builder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(l => l.Author)
                .HasForeignKey(l => l.AuthorId);
        }
    }
}
