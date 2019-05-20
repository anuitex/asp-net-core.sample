using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.Repositories.Interfaces
{
    public interface IBookRepository: IBaseRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(long authorId);
    }
}
