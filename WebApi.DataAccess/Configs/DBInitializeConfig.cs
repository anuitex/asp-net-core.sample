using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Configs
{
    public class DBInitializeConfig
    {
        private readonly IServiceProvider _serviceProvider;

        public DBInitializeConfig(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InitializeDatabase()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                IAuthorRepository authorRepository = serviceScope.ServiceProvider.GetService<IAuthorRepository>();
                IGenreRepository genreRepository = serviceScope.ServiceProvider.GetService<IGenreRepository>();
                IBookRepository bookRepository = serviceScope.ServiceProvider.GetService<IBookRepository>();
                IBookGenreRepository bookGenreRepository = serviceScope.ServiceProvider.GetService<IBookGenreRepository>();
                IAuthorBookRepository authorBookRepository = serviceScope.ServiceProvider.GetService<IAuthorBookRepository>();

                await InitAuthors(authorRepository);
                await InitBooks(bookRepository);
                await InitGenres(genreRepository);
                await InitBookGenre(bookGenreRepository);
                await InitAuthorBook(authorBookRepository);
            }
        }

        private async Task InitAuthors(IAuthorRepository authorRepository)
        {
            int authorsCount = await authorRepository.GetCount();

            if (authorsCount != 0)
            {
                return;
            }

            var authors = new List<Author>
            {
                new Author{ FirstName = "Taras", LastName = "Shevchenko" },
                new Author{ FirstName = "Lesya", LastName = "Ukrainka" },
                new Author{ FirstName = "Vasyl", LastName = "Symonenko" },
                new Author{ FirstName = "Oleksandr", LastName = "Dovzhenko" },
            };

            await authorRepository.AddRangeAsync(authors);
        }

        private async Task InitGenres(IGenreRepository genreRepository)
        {
            int genresCount = await genreRepository.GetCount();

            if (genresCount != 0)
            {
                return;
            }

            var genres = new List<Genre>
            {
                new Genre{ Title = "Prose" },
                new Genre{ Title = "Novel" },
                new Genre{ Title = "Story" },
                new Genre{ Title = "Verse" },
            };

            await genreRepository.AddRangeAsync(genres);
        }

        private async Task InitBooks(IBookRepository bookRepository)
        {
            int booksCount = await bookRepository.GetCount();

            if (booksCount != 0)
            {
                return;
            }

            var books = new List<Book>
            {
                new Book{ Title = "The Forest Song", PublicationYear = 1997 },
                new Book{ Title = "Zvenigora", PublicationYear = 1996 },
                new Book{ Title = "Silence and Thunder", PublicationYear = 1995 },
                new Book{ Title = "Zapovit", PublicationYear = 1994 },
            };

            await bookRepository.AddRangeAsync(books);
        }

        private async Task InitBookGenre(IBookGenreRepository bookGenreRepository)
        {
            int bookGenreCount = await bookGenreRepository.GetCount();

            if (bookGenreCount != 0)
            {
                return;
            }

            var bookGenreList = new List<BookGenre>
            {
               new BookGenre { BookId = 1, GenreId = 1 },
               new BookGenre { BookId = 1, GenreId = 2 },
               new BookGenre { BookId = 1, GenreId = 3 },
               new BookGenre { BookId = 1, GenreId = 4 },
               new BookGenre { BookId = 2, GenreId = 1 },
               new BookGenre { BookId = 2, GenreId = 2 },
               new BookGenre { BookId = 2, GenreId = 3 },
               new BookGenre { BookId = 3, GenreId = 4 },
               new BookGenre { BookId = 3, GenreId = 3 },
               new BookGenre { BookId = 4, GenreId = 2 },
               new BookGenre { BookId = 4, GenreId = 3 },
               new BookGenre { BookId = 4, GenreId = 1 },
            };

            await bookGenreRepository.AddRangeAsync(bookGenreList);
        }

        private async Task InitAuthorBook(IAuthorBookRepository authorBookRepository)
        {
            try
            {
                int authorBookCount = await authorBookRepository.GetCount();

                if (authorBookCount != 0)
                {
                    return;
                }

                var authorBookList = new List<AuthorBook>
            {
                new AuthorBook { AuthorId = 1, BookId = 1 },
                new AuthorBook { AuthorId = 2, BookId = 1 },
                new AuthorBook { AuthorId = 3, BookId = 1 },
                new AuthorBook { AuthorId = 4, BookId = 1 },
                new AuthorBook { AuthorId = 1, BookId = 2 },
                new AuthorBook { AuthorId = 2, BookId = 2 },
                new AuthorBook { AuthorId = 3, BookId = 2 },
                new AuthorBook { AuthorId = 1, BookId = 3 },
                new AuthorBook { AuthorId = 4, BookId = 3 },
                new AuthorBook { AuthorId = 2, BookId = 4 },
                new AuthorBook { AuthorId = 3, BookId = 4 },
                new AuthorBook { AuthorId = 4, BookId = 4 },
            };

                await authorBookRepository.AddRangeAsync(authorBookList);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
