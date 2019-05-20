using System.Threading.Tasks;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.Repositories.Interfaces
{
    public interface IRefreshTokenRepository: IBaseRepository<RefreshToken>
    {
        Task<RefreshToken> GetByAccountIdAsync(long accountId);
        Task<RefreshToken> GetByTokenAsync(string token);
    }
}
