using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Config.Factories.Interfaces
{
    public interface ITokenFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        string GenerateEncodedRefreshToken(int size = 32);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
