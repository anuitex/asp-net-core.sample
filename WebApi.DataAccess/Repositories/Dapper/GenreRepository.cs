using Microsoft.Extensions.Configuration;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories.Dapper
{
    public class GenreRepository: BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(IConfiguration configuration) : base(configuration)
        {
        }


    }
}
