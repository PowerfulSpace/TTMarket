using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Domain;
using TTMarket.Products.Persistence.DatabaseConfig;

namespace TTMarket.Products.Persistence.Repositories
{
    internal class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDBConnection settings)
            : base(settings) { }

        async Task<bool> IProductRepository.CheckNameUniqueAsync(string name,
                                                                 CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Name, name);
            return await _collection.Find(filter).AnyAsync();
        }

        async Task<bool> IProductRepository.CheckNameWhenUpdateUniqueAsync(Guid id,
                                                                           string name,
                                                                           CancellationToken cancellationToken)
        {       
            var filterFirst = Builders<Product>.Filter.Eq(x => x.Id, id) &
                              Builders<Product>.Filter.Eq(x => x.Name, name) |
                              Builders<Product>.Filter.Not(Builders<Product>.Filter.Eq(x => x.Name, name));
            var productFirstExists = await _collection.Find(filterFirst).AnyAsync();
            var filterSecond = Builders<Product>.Filter.Not(Builders<Product>.Filter.Eq(x => x.Id, id)) &
                               Builders<Product>.Filter.Eq(x => x.Name, name);
            var productSecondExists = await _collection.Find(filterSecond).AnyAsync();
            if(productFirstExists && productSecondExists is false)
                return true;
            return false;
        }
    }
}