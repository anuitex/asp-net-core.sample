using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories.EntityFramework
{
    public class RefreshTokenRepository: BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<RefreshToken> GetByAccountIdAsync(long accountId)
        {
            RefreshToken token = await _dbSet.LastOrDefaultAsync(x => x.AccountId == accountId);
            return token;
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            RefreshToken result = await _dbSet.LastOrDefaultAsync(x => x.Token == token);
            return result;
        }
    }
}
