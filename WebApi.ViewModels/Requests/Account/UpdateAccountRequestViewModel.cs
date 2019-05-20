using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.Requests.Account
{
    public class UpdateAccountRequestViewModel
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
        /// Gender
        /// </summary>
        [Required]
        [Display(Name = "Gender")]
        public UpdateAccountRequestViewModelGenderType Gender { get; set; }
    }

    public enum UpdateAccountRequestViewModelGenderType
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
