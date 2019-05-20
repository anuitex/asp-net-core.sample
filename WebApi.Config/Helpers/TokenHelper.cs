using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Config.Models;
using WebApi.Config.Params;

namespace WebApi.Config.Helpers
{
    public class TokenHelper
    {
        public static async Task<Token> GenerateJwt(GenerateJwtParams generateJwtParams)
        {
            var token = new Token
            {
                Id = generateJwtParams.Identity.Claims.Single(c => c.Type == Constants.Strings.JwtClaimIdentifiers.Id).Value,
                AccessToken = await generateJwtParams.TokenFactory.GenerateEncodedToken(generateJwtParams.UserName, generateJwtParams.Identity),
                AccessTokenExpirationDate = DateTime.Now.AddHours((int)generateJwtParams.JwtOptions.ValidFor.TotalSeconds),
            };

            return token;
        }
    }
}
