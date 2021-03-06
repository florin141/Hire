using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hire.Core.Data
{
    /// <summary>
    /// Entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        TEntity GetById(object id);

        /// <summary>
        /// Get entities
        /// </summary>
        /// <returns>Entity list</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }
    }
}
