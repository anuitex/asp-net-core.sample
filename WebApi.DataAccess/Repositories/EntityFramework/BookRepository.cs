using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories.EntityFramework
{
    public class BookRepository: BaseRepository<Book>, IBookRepository
    {
        private readonly ApplicationContext _repositoryContext;

        public BookRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(long authorId)
        {
            List<AuthorBook> authorBooks = await _repositoryContext.Set<AuthorBook>().Where(x => x.AuthorId == authorId).ToListAsync();

            IEnumerable<Book> books = authorBooks.Select(x => new Book
            {
                Id = x.Book.Id,
                Title = x.Book.Title,
                PublicationYear = x.Book.PublicationYear
            });

            return books;
        }
    }
}
