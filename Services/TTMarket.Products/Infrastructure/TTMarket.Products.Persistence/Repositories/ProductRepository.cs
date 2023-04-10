using System;
using System.Linq.Expressions;
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

        async Task<bool> IProductRepository.CheckNameWhenUpdateUniqueAsync(Expression<Func<Product, bool>> filterExpressionFirst,
                                                                           Expression<Func<Product, bool>> filterExpressionSecond,
                                                                           CancellationToken cancellationToken)
        {       
            var productFirstExists = await _collection.Find(filter: filterExpressionFirst)
                                                      .AnyAsync();
            var productSecondExists = await _collection.Find(filter: filterExpressionSecond)
                                                       .AnyAsync();
            if(productFirstExists && productSecondExists is false)
                return true;
            return false;
        }
    }
}