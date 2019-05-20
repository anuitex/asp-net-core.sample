using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories.EntityFramework
{
    public class AuthorBookRepository: BaseRepository<AuthorBook>, IAuthorBookRepository
    {
        public AuthorBookRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
