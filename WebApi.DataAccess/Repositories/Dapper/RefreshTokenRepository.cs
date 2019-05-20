using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Extensions;
using WebApi.DataAccess.Repositories.Interfaces;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace WebApi.DataAccess.Repositories.Dapper
{
    public class RefreshTokenRepository: BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<RefreshToken> GetByAccountIdAsync(long accountId)
        {
            using (IDbConnection connection = Connection)
            {
                string tokensTableName = EntityHelper.GetEntityTableName<RefreshToken>();
                string accountsTableName = EntityHelper.GetEntityTableName<Account>();

                string sQuery = $"SELECT TOP 1 * FROM {tokensTableName} as Tokens INNER JOIN {accountsTableName} as Accounts ON Tokens.AccountId = Accounts.Id WHERE Tokens.AccountId = @AccountId";

                connection.Open();

                IEnumerable<RefreshToken> result = await connection.QueryAsync<RefreshToken, Account, RefreshToken>(
                    sQuery,
                    (refreshToken, account) =>
                    {
                        refreshToken.Account = account;
                        return refreshToken;
                    },
                    new { AccountId = accountId });

                RefreshToken token = result.LastOrDefault();

                connection.Close();

                return token;
            }
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            using (IDbConnection connection = Connection)
            {
                string tokensTableName = EntityHelper.GetEntityTableName<RefreshToken>();
                string accountsTableName = EntityHelper.GetEntityTableName<Account>();

                string sQuery = $"SELECT TOP 1 * FROM {tokensTableName} as Tokens INNER JOIN {accountsTableName} as Accounts ON Tokens.AccountId = Accounts.Id WHERE Tokens.Token = @Token";

                connection.Open();

                IEnumerable<RefreshToken> result = await connection.QueryAsync<RefreshToken, Account, RefreshToken>(
                    sQuery,
                    (refreshTokenEntity, account) =>
                    {
                        refreshTokenEntity.Account = account;
                        return refreshTokenEntity;
                    },
                    new { Token = token });

                RefreshToken refreshToken = result.LastOrDefault();

                connection.Close();

                return refreshToken;
            }
        }
    }
}
