using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories.EntityFramework
{
    public class AuthorRepository: BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}


