using System;
using System.Threading;
using System.Threading.Tasks;
using TTMarket.Products.Domain;

namespace TTMarket.Products.Application.Contracts.Persistence
{
    /// <summary>
    /// Product interface for communicate with the database
    /// </summary>
    /// <typeparam name="T">type of an entity storage in the database</typeparam>
    public interface IProductRepository : IGenericRepository<Product>
    {
        bool CheckNameUnique(string name, CancellationToken cancellationToken);
        Task<bool> CheckNameUniqueAsync(string name, CancellationToken cancellationToken);
        Task<bool> CheckNameWhenUpdateUniqueAsync(Guid id, string name, CancellationToken cancellationToken);
    }
}