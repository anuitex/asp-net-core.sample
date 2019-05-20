using System;

namespace WebApi.ViewModels.Responses.Account
{
    public class RefreshTokenAccountResponseViewModel
    {
        /// <summary>
        /// Account Id
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Access Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Access Token Expiration Date
        /// </summary>
        public DateTime AccessTokenExpirationDate { get; set; }

        /// <summary>
        /// Access Token Expiration Date
        /// </summary>
        public DateTime RefreshTokenExpirationDate { get; set; }
    }
}
