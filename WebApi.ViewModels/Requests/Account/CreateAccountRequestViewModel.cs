using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.Requests.Account
{
    public class CreateAccountRequestViewModel
    {
        /// <summary>
        /// First Name
        /// </summary>
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [Required]
        [Display(Name = "Gender")]
        public CreateAccountRequestViewModelGenderType Gender { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Password must be at least six characters long, should contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Password confirmation
        /// </summary>
        [Required]
        [Compare("Password", ErrorMessage = "Passwords doesn't match")]
        [DataType(DataType.Password)]
        [Display(Name = "Password confirmation")]
        public string PasswordConfirmation { get; set; }
    }

    public enum CreateAccountRequestViewModelGenderType
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
