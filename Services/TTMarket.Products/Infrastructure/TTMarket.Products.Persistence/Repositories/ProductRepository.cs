using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Domain;
using TTMarket.Products.Persistence.DatabaseConfig;

namespace TTMarket.Products.Persistence.Repositories
{
    internal class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDBConnection settings)
            : base(settings) 
        {
        }
    }
}