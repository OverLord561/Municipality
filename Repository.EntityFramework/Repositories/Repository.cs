using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Repositories.EntityFramework.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
       where TEntity : class, new()
    {
        protected readonly DbContext _context;
       

        /// <summary>
        /// Returns the number of elements in a sequence;
        /// </summary>
        public long Count => _context.Set<TEntity>().Count();

        public Repository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            
        }

        protected virtual IQueryable<TEntity> Include()
        {
            return _context.Set<TEntity>();
        }

        /// <summary>
        /// Adds entity into the database.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        /// <returns></returns>
        public virtual int Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Add(entity);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Adds entity into the database.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        /// <returns></returns>
        public virtual async Task<int> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Set<TEntity>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds a collection of entities into the database.
        /// </summary>
        /// <param name="entity">A collection of entities to add.</param>
        /// <returns></returns>
        public virtual int AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Adds a collection of entities into the database.
        /// </summary>
        /// <param name="entity">A collection of entities to add.</param>
        /// <returns></returns>
        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates entity in the database.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            // If entity is not being tracked by the context
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entity);
                _context.Entry<TEntity>(entity).State = EntityState.Modified;
            }
            else // If entity is being tracked by the context
            {
                _context.Set<TEntity>().Update(entity);
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Updates entity in the database.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            // If entity is not being tracked by the context
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entity);
                _context.Entry<TEntity>(entity).State = EntityState.Modified;
            }
            else // If entity is being tracked by the context
            {
                _context.Set<TEntity>().Update(entity);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Filters the sequence of values based on a predicate using the provided mapping engine.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A sequence of values matched by function.</returns>
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Include().Where(predicate).ToList();
        }

        /// <summary>
        /// Filters the sequence of values based on a predicate using the provided mapping engine.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="page">Page number to take.</param>
        /// <param name="pageSize">Page size to take.</param>
        /// <returns>A sequence of values matched by function.</returns>
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, int page, int pageSize)
        {
            return Include().Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

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
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Include().SingleOrDefault(predicate);
        }

        
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
        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Include().SingleOrDefaultAsync(predicate);
        }

        public virtual IEnumerable<TEntity> All()
        {
            return Include().ToList();
        }

 
        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await Include().ToListAsync();
        }
        public virtual int Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChanges();
        }

        public virtual async Task<int> RemoveAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual int RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            return _context.SaveChanges();
        }

        public virtual async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            return await _context.SaveChangesAsync();
        }

        public void DetachAllEntities()
        {
            foreach (var entity in _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
            {
                _context.Entry(entity.Entity).State = EntityState.Detached;
            }
        }

        public async Task<TResult> Max<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null) where TResult : struct
        {
            return predicate != null
                ? await _context.Set<TEntity>().AsNoTracking().Where(predicate).MaxAsync(selector)
                : await _context.Set<TEntity>().AsNoTracking().MaxAsync(selector);
        }
      


        public virtual (IEnumerable<TEntity>, int) Get(int page, int size)
        {
            var total = _context.Set<TEntity>().Count();
            var items = Include().Skip((page - 1) * size)
                                           .Take(size);
        
            return (items, total);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Include().Where(predicate).ToListAsync();
        }

    }
}
