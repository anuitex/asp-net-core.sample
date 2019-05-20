using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Extensions;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories.Dapper
{
    public class BookRepository: BaseRepository<Book>, IBookRepository
    {
        public BookRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(long authorId)
        {
            using (IDbConnection connection = Connection)
            {
                string authorBooksTableName = EntityHelper.GetEntityTableName<AuthorBook>();
                string booksTableName = EntityHelper.GetEntityTableName<Book>();

                string sQuery = $@"SELECT B.Id, B.PublicationYear, B.Title FROM {booksTableName} as B 
                                    INNER JOIN {authorBooksTableName} as AB ON AB.BookId = B.Id
                                    WHERE AB.AuthorId = @AuthorId";

                connection.Open();

                IEnumerable<Book> result = await connection.QueryAsync<Book>(
                    sQuery, new { AuthorId = authorId });

                connection.Close();

                return result;
            }
        }
    }
}
