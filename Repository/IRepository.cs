using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository<TEntity> where TEntity:class, new()
    {
        /// <summary>
        /// Returns the number of elements in a sequence;
        /// </summary>
        long Count { get; }

        /// <summary>
        /// Adds entity into the database.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        /// <returns></returns>
        int Add(TEntity entity);

        /// <summary>
        /// Adds entity into the database.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        /// <returns></returns>
        Task<int> AddAsync(TEntity entity);

        /// <summary>
        /// Adds a collection of entities into the database.
        /// </summary>
        /// <param name="entity">A collection of entities to add.</param>
        /// <returns></returns>
        int AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Adds a collection of entities into the database.
        /// </summary>
        /// <param name="entity">A collection of entities to add.</param>
        /// <returns></returns>
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates entity in the database.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates entity in the database.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Filters the sequence of values based on a predicate using the provided mapping engine.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A sequence of values matched by function.</returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        (IEnumerable<TEntity>,int) Get(int page, int size);

        /// <summary>
        /// Filters the sequence of values based on a predicate using the provided mapping engine.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="page">Page number to take.</param>
        /// <param name="pageSize">Page size to take.</param>
        /// <returns>A sequence of values matched by function.</returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, int page, int pageSize);

        
        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty;
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <returns>
        /// The single element of the input sequence, or <see cref="default(TSource)"/> if the sequence
        /// containts no element.
        /// </returns>
        /// <exception cref="ArgumentNullException">source is null.</exception>
        /// <exception cref="InvalidOperationException">source has more than one element.</exception>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
       
        /// <summary>
        /// Asynchronous returns the only element of a sequence that satisfies condition or a default if no such element exists;
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <returns>
        /// The single element of the input sequence, or <see cref="default(TSource)"/> if the sequence
        /// containts no element.
        /// </returns>
        /// <exception cref="ArgumentNullException">source is null.</exception>
        /// <exception cref="InvalidOperationException">source has more than one element.</exception>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Asynchronous returns a projection of the only element of a sequence that satisfies condition using 
        /// the provided mapping engine or a default if no such element exists;
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <returns>
        /// The single element of the input sequence, or <see cref="default(TSource)"/> if the sequence
        /// containts no element.
        /// </returns>
        /// <exception cref="ArgumentNullException">source is null.</exception>
        /// <exception cref="InvalidOperationException">source has more than one element.</exception>

        IEnumerable<TEntity> All();


        Task<IEnumerable<TEntity>> AllAsync();


        /// <summary>
        /// Removes entity from the database.
        /// </summary>
        /// <param name="entity">Entity to remove.</param>
        /// <returns></returns>
        int Remove(TEntity entity);

        /// <summary>
        /// Removes entity from the database.
        /// </summary>
        /// <param name="entity">Entity to remove.</param>
        /// <returns></returns>
        Task<int> RemoveAsync(TEntity entity);

        /// <summary>
        /// Removes a collection of entities from the database.
        /// </summary>
        /// <param name="entities">Collection of entities to remove.</param>
        /// <returns></returns>
        int RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Removes a collection of entities from the database.
        /// </summary>
        /// <param name="entities">Collection of entities to remove.</param>
        /// <returns></returns>
        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities);

        void DetachAllEntities();

        Task<TResult> Max<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null) where TResult : struct;

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
