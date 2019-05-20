using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.Requests.Account
{
    public class RefreshTokenAccountRequestViewModel
    {
        /// <summary>
        /// Refresh Token
        /// </summary>
        [Required]
        [Display(Name = "Refresh Token")]
        public string RefreshToken { get; set; }
    }
}
