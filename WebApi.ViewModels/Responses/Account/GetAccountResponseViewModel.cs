namespace WebApi.ViewModels.Responses.Account
{
    public class GetAccountResponseViewModel
    {
        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public GetAccountResponseViewModelGenderType Gender { get; set; }
    }

    public enum GetAccountResponseViewModelGenderType
    {
        /// <summary>
        /// Undefined Gender
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Male Gender
        /// </summary>
        Male = 1,

        /// <summary>
        /// Female Gender
        /// </summary>
        Female = 2
    }
}
