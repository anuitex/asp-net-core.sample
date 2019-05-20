using System.Collections.Generic;

namespace WebApi.ViewModels.Responses.Author
{
    public class GetAuthorResponseViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Books
        /// </summary>
        public IEnumerable<BookViewModelItem> Books { get; set; }
    }

    public class BookViewModelItem
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Publication Year
        /// </summary>
        public int PublicationYear { get; set; }

        /// <summary>
        /// Genres
        /// </summary>
        public IEnumerable<GenreViewModelItem> Genres { get; set; }
    }

    public class GenreViewModelItem
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
    }
}
