using System;
using System.Collections.Generic;
using System.Linq;
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
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(typeof(TDocument).Name);
        }

        IQueryable<TDocument> IGenericRepository<TDocument>.AsQueryable()
            => _collection.AsQueryable();

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

        List<TProjected> IGenericRepository<TDocument>.FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression,
                                                                            Expression<Func<TDocument, TProjected>> projectionExpression,
                                                                            CancellationToken cancellationToken)
            => _collection.Find(filter: filterExpression)
                          .Project(projection: projectionExpression)
                          .ToList(cancellationToken: cancellationToken);

        async Task<TDocument> IGenericRepository<TDocument>.FindOneAsync(Expression<Func<TDocument, bool>> filterExpression,
                                                                         CancellationToken cancellationToken)
            => await _collection.Find(filter: filterExpression)
                                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        async Task<List<TDocument>> IGenericRepository<TDocument>.GetAllAsync(CancellationToken cancellationToken)
            => await _collection.Find(filter: x => true)
                                .ToListAsync(cancellationToken: cancellationToken);

        async Task IGenericRepository<TDocument>.InsertManyAsync(ICollection<TDocument> documents,
                                                                 CancellationToken cancellationToken)
        {
            foreach (var item in documents)
            {
                item.Created = DateTime.Now;
                item.Updated = null;
            }
            await _collection.InsertManyAsync(documents: documents,
                                              cancellationToken: cancellationToken);
        }

        async Task IGenericRepository<TDocument>.InsertOneAsync(TDocument document,
                                                                CancellationToken cancellationToken)
        {
            document.Created = DateTime.Now;
            document.Updated = null;
            await _collection.InsertOneAsync(document: document,
                                             cancellationToken: cancellationToken);
        }

        async Task IGenericRepository<TDocument>.ReplaceOneAsync(TDocument document,
                                                                 CancellationToken cancellationToken)
        {
            document.Updated = DateTime.Now;
            await _collection.ReplaceOneAsync(filter: Builders<TDocument>.Filter
                                                                         .Eq(x => x.Id, document.Id),
                                              replacement: document);
        }
    }
}