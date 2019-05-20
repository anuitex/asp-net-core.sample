using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.Requests.Account
{
    public class ChangePasswordAccountRequestViewModel
    {
        ///// <summary>
        ///// Current Password
        ///// </summary>
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Password must be at least six characters long, should contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// New Password
        /// </summary>
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Password must be at least six characters long, should contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
    }
}
