using System.Collections.Generic;

namespace WebApi.ViewModels.Responses.Author
{
    public class GetAuthorListAuthorResponseViewModel
    {
        /// <summary>
        /// Authors
        /// </summary>
        public IEnumerable<AuthorViewModelItem> AuthorList { get; set; }
    }

    public class AuthorViewModelItem
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
    }
}
