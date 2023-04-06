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
            _collection = database.GetCollection<TDocument>(nameof(TDocument));
        }

        public IQueryable<TDocument> AsQueryable()
            => _collection.AsQueryable();

        public void DeleteById(Guid id,
                               CancellationToken cancellationToken)
            => _collection.FindOneAndDelete(filter: Builders<TDocument>.Filter
                                                                       .Eq(x => x.Id, id), 
                                            cancellationToken: cancellationToken);

        public async Task DeleteByIdAsync(Guid id,
                                          CancellationToken cancellationToken)
            => await _collection.FindOneAndDeleteAsync(filter: Builders<TDocument>.Filter
                                                                                  .Eq(x => x.Id, id), 
                                                       cancellationToken: cancellationToken);

        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression,
                               CancellationToken cancellationToken)
            => _collection.DeleteMany(filter: filterExpression, 
                                      cancellationToken: cancellationToken);

        public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression,
                                    CancellationToken cancellationToken)
            => await _collection.DeleteManyAsync(filter: filterExpression, 
                                                 cancellationToken: cancellationToken);

        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression,
                              CancellationToken cancellationToken)
            => _collection.DeleteOne(filter: filterExpression, 
                                     cancellationToken: cancellationToken);

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression,
                                   CancellationToken cancellationToken)
            => _collection.DeleteOneAsync(filter: filterExpression, 
                                          cancellationToken: cancellationToken);

        public void Exists(Guid id,
                           CancellationToken cancellationToken)
            => _collection.Find(filter: Builders<TDocument>.Filter
                                                           .Eq(x => x.Id, id))
                          .Any(cancellationToken: cancellationToken);

        public async Task<bool> ExistsAsync(Guid id,
                                      CancellationToken cancellationToken)
            => await _collection.Find(filter: Builders<TDocument>.Filter
                                                                 .Eq(x => x.Id, id))
                                .AnyAsync(cancellationToken: cancellationToken);

        public IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression,
                                               CancellationToken cancellationToken)
            => _collection.Find(filter: filterExpression)
                          .ToList(cancellationToken: cancellationToken);

        public IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression,
                                                            Expression<Func<TDocument, TProjected>> projectionExpression,
                                                            CancellationToken cancellationToken)
            => _collection.Find(filter: filterExpression)
                          .Project(projection: projectionExpression)
                          .ToList(cancellationToken: cancellationToken);

        public TDocument FindById(Guid id,
                                  CancellationToken cancellationToken)
            => _collection.Find(filter: Builders<TDocument>.Filter
                                                           .Eq(x => x.Id, id))
                          .SingleOrDefault(cancellationToken: cancellationToken);

        public async Task<TDocument> FindByIdAsync(Guid id,
                                                   CancellationToken cancellationToken)
            => await _collection.Find(filter: Builders<TDocument>.Filter
                                                                 .Eq(x => x.Id, id))
                                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

        public TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression,
                                 CancellationToken cancellationToken)
            => _collection.Find(filter: filterExpression)
                          .FirstOrDefault(cancellationToken: cancellationToken);

        public async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression,
                                                  CancellationToken cancellationToken)
            => await _collection.Find(filter: filterExpression)
                                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        public IEnumerable<TDocument> GetAll(CancellationToken cancellationToken)
            => _collection.Find(filter: x => true)
                          .ToList(cancellationToken: cancellationToken);

        public async Task<IEnumerable<TDocument>> GetAllAsync(CancellationToken cancellationToken)
            => await _collection.Find(filter: x => true)
                                .ToListAsync(cancellationToken: cancellationToken);

        public void InsertMany(ICollection<TDocument> documents,
                               CancellationToken cancellationToken)
            => _collection.InsertMany(documents: documents,
                                      cancellationToken: cancellationToken);

        public async Task InsertManyAsync(ICollection<TDocument> documents,
                                          CancellationToken cancellationToken)
            => await _collection.InsertManyAsync(documents: documents,
                                                 cancellationToken: cancellationToken);

        public void InsertOne(TDocument document,
                              CancellationToken cancellationToken)
            => _collection.InsertOne(document: document,
                                     cancellationToken: cancellationToken);

        public async Task InsertOneAsync(TDocument document,
                                         CancellationToken cancellationToken)
            => await _collection.InsertOneAsync(document: document,
                                                cancellationToken: cancellationToken);

        public void ReplaceOne(TDocument document,
                               CancellationToken cancellationToken)
            => _collection.ReplaceOne(filter: Builders<TDocument>.Filter
                                                                 .Eq(x => x.Id, document.Id), 
                                      replacement: document);

        public async Task ReplaceOneAsync(TDocument document,
                                    CancellationToken cancellationToken)
            => await _collection.ReplaceOneAsync(filter: Builders<TDocument>.Filter
                                                                            .Eq(x => x.Id, document.Id),
                                                 replacement: document);
    }
}