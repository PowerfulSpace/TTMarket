using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TTMarket.Products.Domain.Common;

namespace TTMarket.Products.Application.Contracts.Persistence
{
    /// <summary>
    /// Generic interface for communicate with the database
    /// </summary>
    /// <typeparam name="T">type of an entity storage in the database</typeparam>
    public interface IGenericRepository<T> : IDisposable where T : BaseEntity
    {
        /// <summary>
        /// Get all entities from the database
        /// </summary>
        /// <param name="token">Cancellation Token</param>
        /// <returns>IReadOnlyList entities</returns>
        Task<IReadOnlyList<T>> GetAsync(CancellationToken token);

        /// <summary>
        /// Get an entity from the database by id
        /// </summary>
        /// <param name="id">Guid of the product</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Entity</returns>
        Task<T> GetByIdAsync(Guid id, CancellationToken token);

        /// <summary>
        /// Create an entity in the database
        /// </summary>
        /// <param name="entity">Product</param>
        /// <param name="token">Cancellation Token</param>
        Task CreateAsync(T entity, CancellationToken token);

        /// <summary>
        /// Update an entity in the database
        /// </summary>
        /// <param name="entity">Product</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>If success return true otherwise false</returns>
        Task UpdateAsync(T entity, CancellationToken token);

        /// <summary>
        /// Delete an entity from the database by id
        /// </summary>
        /// <param name="id">Guid of the product</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>If success return true otherwise false</returns>
        Task DeleteAsync(Guid id, CancellationToken token);

        /// <summary>
        /// Check if entity is exists in the database
        /// </summary>
        /// <param name="id">Guid of the product</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>If success return true otherwise false</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken token);
    }
}