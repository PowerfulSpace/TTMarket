using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TTMarket.Catalogs.Domain.Common;

namespace TTMarket.Catalogs.Application.Contracts.Persistence
{
    public interface IGenericRepository<TEntity> where TEntity : IBaseEntity
    {
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression,
                                   CancellationToken cancellationToken);

        Task InsertOneAsync(TEntity document,
                            CancellationToken cancellationToken);

        Task InsertManyAsync(ICollection<TEntity> documents,
                             CancellationToken cancellationToken);

        Task ReplaceOneAsync(Expression<Func<TEntity, bool>> filterExpression,
                             TEntity document,
                             CancellationToken cancellationToken);

        Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression,
                            CancellationToken cancellationToken);

        Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression,
                             CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filterExpression,
                               CancellationToken cancellationToken);
    }
}