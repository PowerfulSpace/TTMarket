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

        bool IProductRepository.CheckNameUnique(string name,
                                                CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Name, name);
            return _collection.Find(filter).Any();
        }

        async Task<bool> IProductRepository.CheckNameUniqueAsync(string name,
                                                                 CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Name, name);
            return await _collection.Find(filter).AnyAsync();
        }
    }
}