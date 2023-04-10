using System;
using System.Linq.Expressions;
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
        Task<bool> CheckNameWhenUpdateUniqueAsync(Expression<Func<Product, bool>> filterExpressionFirst,
                                                  Expression<Func<Product, bool>> filterExpressionSecond,
                                                  CancellationToken cancellationToken);
    }
}