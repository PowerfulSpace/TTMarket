using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TTMarket.Products.Domain.Common;

namespace TTMarket.Products.Application.Contracts.Persistence
{
    /// <summary>
    /// Generic interface for communicate with the database
    /// </summary>
    /// <typeparam name="T">type of an entity storage in the database</typeparam>
    public interface IGenericRepository<TDocument> where TDocument : IDocument
    {
        Task<List<TDocument>> GetAllAsync(CancellationToken cancellationToken);

        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression,
                                     CancellationToken cancellationToken);

        Task InsertOneAsync(TDocument document,
                            CancellationToken cancellationToken);

        Task InsertManyAsync(ICollection<TDocument> documents,
                             CancellationToken cancellationToken);

        Task ReplaceOneAsync(Expression<Func<TDocument, bool>> filterExpression,
                             TDocument document,
                             CancellationToken cancellationToken);

        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression,
                            CancellationToken cancellationToken);

        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression,
                             CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> filterExpression,
                               CancellationToken cancellationToken);
    }
}