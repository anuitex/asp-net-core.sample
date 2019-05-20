using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess
{
    public class ApplicationContext: IdentityDbContext<Account, AccountRole, long>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<AuthorBook> AuthorBooks { get; set; }

        public DbSet<BookGenre> BookGenres { get; set; }
    }
}
