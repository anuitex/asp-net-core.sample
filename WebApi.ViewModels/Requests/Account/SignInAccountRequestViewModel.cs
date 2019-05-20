using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.Requests.Account
{
    public class SignInAccountRequestViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Password must be at least six characters long, should contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
