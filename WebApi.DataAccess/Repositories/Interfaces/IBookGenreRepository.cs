using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.Repositories.Interfaces
{
    public interface IBookGenreRepository: IBaseRepository<BookGenre>
    {
        Task<IEnumerable<BookGenre>> GetBooksGenresByBookIdListAsync(IEnumerable<long> booksIdList);
    }
}
