using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Extensions;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories.Dapper
{
    public class BookGenreRepository: BaseRepository<BookGenre>, IBookGenreRepository
    {
        public BookGenreRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<BookGenre>> GetBooksGenresByBookIdListAsync(IEnumerable<long> booksIdList)
        {
            using (IDbConnection connection = Connection)
            {
                string booksGenreTableName = EntityHelper.GetEntityTableName<BookGenre>();
                string genreTableName = EntityHelper.GetEntityTableName<Genre>();

                string sQuery = $@"SELECT * FROM {booksGenreTableName} as BG 
                                    INNER JOIN {genreTableName} as G ON BG.GenreId = G.Id
                                    WHERE BG.BookId IN @BookIdList";

                connection.Open();

                IEnumerable<BookGenre> result = await connection.QueryAsync<BookGenre, Genre, BookGenre>(
                    sQuery,
                    (bookGenre, genre) =>
                    {
                        bookGenre.Genre = genre;
                        return bookGenre;
                    },
                    new { BookIdList = booksIdList });

                connection.Close();

                return result;
            }
        }
    }
}
