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

        void IGenericRepository<TDocument>.DeleteById(Guid id,
                                                      CancellationToken cancellationToken)
            => _collection.FindOneAndDelete(filter: Builders<TDocument>.Filter
                                                                       .Eq(x => x.Id, id), 
                                            cancellationToken: cancellationToken);

        async Task IGenericRepository<TDocument>.DeleteByIdAsync(Guid id,
                                                                 CancellationToken cancellationToken)
            => await _collection.FindOneAndDeleteAsync(filter: Builders<TDocument>.Filter
                                                                                  .Eq(x => x.Id, id), 
                                                       cancellationToken: cancellationToken);

        void IGenericRepository<TDocument>.DeleteMany(Expression<Func<TDocument, bool>> filterExpression,
                                                      CancellationToken cancellationToken)
            => _collection.DeleteMany(filter: filterExpression, 
                                      cancellationToken: cancellationToken);

        async Task IGenericRepository<TDocument>.DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression,
                                                                 CancellationToken cancellationToken)
            => await _collection.DeleteManyAsync(filter: filterExpression, 
                                                 cancellationToken: cancellationToken);

        void IGenericRepository<TDocument>.DeleteOne(Expression<Func<TDocument, bool>> filterExpression,
                                                     CancellationToken cancellationToken)
            => _collection.DeleteOne(filter: filterExpression, 
                                     cancellationToken: cancellationToken);

        Task IGenericRepository<TDocument>.DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression,
                                                          CancellationToken cancellationToken)
            => _collection.DeleteOneAsync(filter: filterExpression, 
                                          cancellationToken: cancellationToken);

        void IGenericRepository<TDocument>.Exists(Guid id,
                                                  CancellationToken cancellationToken)
            => _collection.Find(filter: Builders<TDocument>.Filter
                                                           .Eq(x => x.Id, id))
                          .Any(cancellationToken: cancellationToken);

        async Task<bool> IGenericRepository<TDocument>.ExistsAsync(Guid id,
                                                                   CancellationToken cancellationToken)
            => await _collection.Find(filter: Builders<TDocument>.Filter
                                                                 .Eq(x => x.Id, id))
                                .AnyAsync(cancellationToken: cancellationToken);

        IEnumerable<TDocument> IGenericRepository<TDocument>.FilterBy(Expression<Func<TDocument, bool>> filterExpression,
                                                                      CancellationToken cancellationToken)
            => _collection.Find(filter: filterExpression)
                          .ToList(cancellationToken: cancellationToken);

        IEnumerable<TProjected> IGenericRepository<TDocument>.FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression,
                                                                                   Expression<Func<TDocument, TProjected>> projectionExpression,
                                                                                   CancellationToken cancellationToken)
            => _collection.Find(filter: filterExpression)
                          .Project(projection: projectionExpression)
                          .ToList(cancellationToken: cancellationToken);

        TDocument IGenericRepository<TDocument>.FindById(Guid id,
                                                         CancellationToken cancellationToken)
            => _collection.Find(filter: Builders<TDocument>.Filter
                                                           .Eq(x => x.Id, id))
                          .SingleOrDefault(cancellationToken: cancellationToken);

        async Task<TDocument> IGenericRepository<TDocument>.FindByIdAsync(Guid id,
                                                                          CancellationToken cancellationToken)
            => await _collection.Find(filter: Builders<TDocument>.Filter
                                                                 .Eq(x => x.Id, id))
                                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

        TDocument IGenericRepository<TDocument>.FindOne(Expression<Func<TDocument, bool>> filterExpression,
                                                        CancellationToken cancellationToken)
            => _collection.Find(filter: filterExpression)
                          .FirstOrDefault(cancellationToken: cancellationToken);

        async Task<TDocument> IGenericRepository<TDocument>.FindOneAsync(Expression<Func<TDocument, bool>> filterExpression,
                                                                         CancellationToken cancellationToken)
            => await _collection.Find(filter: filterExpression)
                                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        IEnumerable<TDocument> IGenericRepository<TDocument>.GetAll(CancellationToken cancellationToken)
            => _collection.Find(filter: x => true)
                          .ToList(cancellationToken: cancellationToken);

        async Task<IEnumerable<TDocument>> IGenericRepository<TDocument>.GetAllAsync(CancellationToken cancellationToken)
            => await _collection.Find(filter: x => true)
                                .ToListAsync(cancellationToken: cancellationToken);

        void IGenericRepository<TDocument>.InsertMany(ICollection<TDocument> documents,
                                                      CancellationToken cancellationToken)
        {
            foreach (var item in documents)
            {
                item.Created = DateTime.Now;
                item.Updated = null;
            }
            _collection.InsertMany(documents: documents,
                                   cancellationToken: cancellationToken);
        }

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

        void IGenericRepository<TDocument>.InsertOne(TDocument document,
                                                     CancellationToken cancellationToken)
        {
            document.Created = DateTime.Now;
            document.Updated = null;
            _collection.InsertOne(document: document,
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

        void IGenericRepository<TDocument>.ReplaceOne(TDocument document,
                                                      CancellationToken cancellationToken)
        {
            document.Updated = DateTime.Now;
            _collection.ReplaceOne(filter: Builders<TDocument>.Filter
                                                              .Eq(x => x.Id, document.Id), 
                                   replacement: document);
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