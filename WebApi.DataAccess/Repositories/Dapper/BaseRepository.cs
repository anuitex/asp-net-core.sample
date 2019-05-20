using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Exceptions;
using WebApi.DataAccess.Extensions;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories.Dapper
{
    public abstract class BaseRepository<TEntity>: IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected IDbConnection Connection => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        public async Task AddAsync(TEntity entity)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                await connection.InsertAsync(entity);

                connection.Close();
            }
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                await connection.InsertAsync(entities);

                connection.Close();
            }
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                IEnumerable<TEntity> entities = await connection.GetAllAsync<TEntity>();

                connection.Close();

                return entities;
            }
        }

        public async Task<TEntity> GetAsync(long id)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                TEntity entity = await connection.GetAsync<TEntity>(id);

                connection.Close();

                return entity;
            }
        }

        public async Task<int> GetCount()
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                string tableName = EntityHelper.GetEntityTableName<TEntity>();

                int count = await connection
                    .ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {tableName}");

                connection.Close();

                return count;
            }
        }

        public async Task<IEnumerable<TEntity>> GetRangeAsync(IEnumerable<long> entityIdList)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                string tableName = EntityHelper.GetEntityTableName<TEntity>();

                IEnumerable<TEntity> entities = await connection
                    .QueryAsync<TEntity>($"SELECT * FROM {tableName} WHERE Id IN @Ids", new { Ids = entityIdList });

                connection.Close();

                return entities;
            }
        }

        public async Task RemoveAsync(TEntity entity)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                await connection.DeleteAsync(entity);

                connection.Close();
            }
        }

        public async Task RemoveAsync(long id)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                string tableName = EntityHelper.GetEntityTableName<TEntity>();

                await connection.ExecuteAsync($"DELETE FROM {tableName} WHERE Id=@Id", new { Id = id });

                connection.Close();
            }
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                bool isSuccess = await connection.DeleteAsync(entities);

                connection.Close();
            }
        }

        public async Task RemoveRangeAsync(IEnumerable<long> entityIdList)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                string tableName = EntityHelper.GetEntityTableName<TEntity>();

                await connection.ExecuteAsync($"DELETE FROM {tableName} WHERE Id IN @Ids", new { Ids = entityIdList });

                connection.Close();
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                await connection.UpdateAsync(entity);

                connection.Close();
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    var entitiesWithErrors = new List<TEntity>();

                    foreach (TEntity entity in entities)
                    {
                        try
                        {
                            bool isUpdateSucceed = await connection.UpdateAsync(entity, transaction: transaction);

                            if (!isUpdateSucceed)
                            {
                                throw new Exception();
                            }
                        }
                        catch
                        {
                            entitiesWithErrors.Add(entity);
                        }
                    }

                    if (entitiesWithErrors.Any())
                    {
                        transaction.Rollback();
                        throw new BulkOperationException("Bulk update failed.", entitiesWithErrors);
                    }

                    transaction.Commit();
                }

                connection.Close();
            }
        }
    }
}
