using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Domain.Common;
using TTMarket.Products.Persistence.DatabaseConfig;

namespace TTMarket.Products.Persistence.Repositories
{
    internal class GenericRepository<TDocument> : IGenericRepository<TDocument> where TDocument : IDocument
    {
        protected readonly IMongoCollection<TDocument> _collection;

        public GenericRepository(IMongoDBConnection settings)
            => _collection 
            = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName)
                                                        .GetCollection<TDocument>(typeof(TDocument).Name);

        async Task IGenericRepository<TDocument>.DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression,
                                                                 CancellationToken cancellationToken)
            => await _collection.DeleteManyAsync(filter: filterExpression, 
                                                 cancellationToken: cancellationToken);

        Task IGenericRepository<TDocument>.DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression,
                                                          CancellationToken cancellationToken)
            => _collection.DeleteOneAsync(filter: filterExpression, 
                                          cancellationToken: cancellationToken);

        async Task<bool> IGenericRepository<TDocument>.ExistsAsync(Expression<Func<TDocument, bool>> filterExpression,
                                                                   CancellationToken cancellationToken)
            => await _collection.Find(filter: filterExpression)
                                .AnyAsync(cancellationToken: cancellationToken);

        async Task<TDocument> IGenericRepository<TDocument>.FindOneAsync(Expression<Func<TDocument, bool>> filterExpression,
                                                                         CancellationToken cancellationToken)
            => await _collection.Find(filter: filterExpression)
                                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        async Task<List<TDocument>> IGenericRepository<TDocument>.GetAllAsync(CancellationToken cancellationToken)
            => await _collection.Find(filter: x => true)
                                .ToListAsync(cancellationToken: cancellationToken);

        async Task IGenericRepository<TDocument>.InsertManyAsync(ICollection<TDocument> documents,
                                                                 CancellationToken cancellationToken)
            => await _collection.InsertManyAsync(documents: documents,
                                                 cancellationToken: cancellationToken);

        async Task IGenericRepository<TDocument>.InsertOneAsync(TDocument document,
                                                                CancellationToken cancellationToken)
            => await _collection.InsertOneAsync(document: document,
                                                cancellationToken: cancellationToken);

        async Task IGenericRepository<TDocument>.ReplaceOneAsync(Expression<Func<TDocument, bool>> filterExpression,
                                                                 TDocument document,
                                                                 CancellationToken cancellationToken)
            => await _collection.ReplaceOneAsync(filter: filterExpression,
                                                 replacement: document);
    }
}