using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Exceptions;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories.EntityFramework
{
    public abstract class BaseRepository<TEntity>: IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private ApplicationContext _applicationContext { get; set; }

        protected DbSet<TEntity> _dbSet { get; set; }

        public BaseRepository(ApplicationContext repositoryContext)
        {
            _applicationContext = repositoryContext;
            _dbSet = _applicationContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            await SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);

            await SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            List<TEntity> list = await _dbSet.ToListAsync();

            return list;
        }

        public async Task<TEntity> GetAsync(long id)
        {
            TEntity entity = await _dbSet.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetRangeAsync(IEnumerable<long> entityIdList)
        {
            List<TEntity> entityList = await _dbSet.AsQueryable().Where(x => entityIdList.Contains(x.Id)).ToListAsync();

            return entityList;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);

            await SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);

            await SaveChangesAsync();
        }

        public async Task RemoveAsync(long id)
        {
            TEntity entity = await GetAsync(id);

            await RemoveAsync(entity);
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);

            await SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<long> entityIdList)
        {
            IEnumerable<TEntity> entities = await GetRangeAsync(entityIdList);

            await RemoveRangeAsync(entities);
        }

        private async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<int> GetCount()
        {
            int count = await _dbSet.CountAsync();

            return count;
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            IDbContextTransaction transaction;

            using (transaction = _applicationContext.Database.BeginTransaction())
            {
                var entitiesWithErrors = new List<TEntity>();

                foreach (TEntity entity in entities)
                {
                    try
                    {
                        _dbSet.Update(entity);
                        await _applicationContext.SaveChangesAsync();
                    }
                    catch(Exception ex)
                    {
                        entitiesWithErrors.Add(entity);
                        transaction.Rollback();

                        ResetContext();

                        transaction = _applicationContext.Database.BeginTransaction();
                    }
                }

                if (entitiesWithErrors.Any())
                {
                    transaction.Rollback();
                    throw new BulkOperationException("Bulk update failed.", entitiesWithErrors);
                }

                if (!entitiesWithErrors.Any())
                {
                    transaction.Commit();
                }
            }
        }

        private void ResetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            DbContextOptions<ApplicationContext> options = optionsBuilder
                    .UseSqlServer("Server=10.10.0.216; Database=AspNetCoreIdentityWebApi; User Id=admin; Password =admin; Trusted_Connection=False;")
                    .Options;

            _applicationContext = new ApplicationContext(options);
        }
    }
}
