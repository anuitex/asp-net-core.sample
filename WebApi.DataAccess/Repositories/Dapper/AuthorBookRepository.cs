using Microsoft.Extensions.Configuration;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories.Dapper
{
    public class AuthorBookRepository: BaseRepository<AuthorBook>, IAuthorBookRepository
    {
        public AuthorBookRepository(IConfiguration configuration) : base(configuration)
        {
        }


    }
}
