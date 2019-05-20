using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> GetAsync();

        Task<TEntity> GetAsync(long id);

        Task<IEnumerable<TEntity>> GetRangeAsync(IEnumerable<long> entityIdList);

        Task UpdateAsync(TEntity entity);

        Task RemoveAsync(TEntity entity);

        Task RemoveAsync(long id);

        Task RemoveRangeAsync(IEnumerable<TEntity> entities);

        Task RemoveRangeAsync(IEnumerable<long> entityIdList);

        Task<int> GetCount();

        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    }
}
