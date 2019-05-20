using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.BusinessLogic.Exceptions;
using WebApi.BusinessLogic.Services.Interfaces;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.ViewModels.Responses.Author;

namespace WebApi.BusinessLogic.Services
{
    public class AuthorService: IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBookGenreRepository _bookGenreRepository;
        private readonly IMapper _mapper;

        public AuthorService(
            IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            IBookGenreRepository bookGenreRepository,
            IMapper mapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _bookGenreRepository = bookGenreRepository;
            _mapper = mapper;
        }

        public async Task<GetAuthorResponseViewModel> GetAuthor(long authorId)
        {
            Author author = await _authorRepository.GetAsync(authorId);

            if (author == null)
            {
                throw new NotFoundException("Author with provided id was not found.");
            }

            GetAuthorResponseViewModel getAuthorResponseViewModel = _mapper.Map<Author, GetAuthorResponseViewModel>(author);

            IEnumerable<Book> authorBooks = await _bookRepository.GetBooksByAuthorIdAsync(authorId);

            if (authorBooks == null || authorBooks.Count() == 0)
            {
                return getAuthorResponseViewModel;
            }

            IEnumerable<long> bookIdList = authorBooks.Select(b => b.Id);

            IEnumerable<BookGenre> booksGenres = await _bookGenreRepository.GetBooksGenresByBookIdListAsync(bookIdList);

            IEnumerable<IGrouping<long, BookGenre>> groupedGenres = booksGenres.GroupBy(bg => bg.BookId);

            getAuthorResponseViewModel.Books = authorBooks.Select(b =>
            {
                IEnumerable<Genre> genres = groupedGenres.Where(g => g.Key == b.Id).SelectMany(g => g.Select(bg => bg.Genre));

                var book = new BookViewModelItem
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublicationYear = b.PublicationYear,
                    Genres = _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreViewModelItem>>(genres)
                };

                return book;

            }).ToList();

            return getAuthorResponseViewModel;
        }

        public async Task Delete(long id)
        {
            await _authorRepository.RemoveAsync(id);
        }

        public async Task<GetAuthorListAuthorResponseViewModel> GetAuthorList()
        {
            IEnumerable<Author> authors = await _authorRepository.GetAsync();

            IEnumerable<AuthorViewModelItem> authorViewModelItemList = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModelItem>>(authors);

            var getAuthorListAuthorResponseViewModel = new GetAuthorListAuthorResponseViewModel
            {
                AuthorList = authorViewModelItemList
            };

            return getAuthorListAuthorResponseViewModel;
        }
    }
}
