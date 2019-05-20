using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories.EntityFramework
{
    public class BookGenreRepository: BaseRepository<BookGenre>, IBookGenreRepository
    {
        public BookGenreRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<BookGenre>> GetBooksGenresByBookIdListAsync(IEnumerable<long> booksIdList)
        {
            List<BookGenre> bookGenres = await _dbSet.Where(x => booksIdList.Contains(x.BookId)).AsNoTracking().ToListAsync();

            return bookGenres;
        }
    }
}
