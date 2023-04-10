using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Contracts.Persistence
{
    /// <summary>
    /// Product interface for communicate with the database
    /// </summary>
    /// <typeparam name="T">type of an entity storage in the database</typeparam>
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}