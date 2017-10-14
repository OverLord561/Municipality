using AutoMapper.QueryableExtensions;
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

        protected virtual string[] HandleException(Exception ex)
        {
            if (ex is DbUpdateException)
            {
                var dbUpdateEx = ex as DbUpdateException;

                var sqlEx = dbUpdateEx?.InnerException?.InnerException as SqlException;

                if (sqlEx != null)
                {
                    switch (sqlEx.Number)
                    {
                        case 2627: // Unique constraint error
                        case 547:  // Constraint check violation
                        case 2601: // Duplicate key row error
                            string[] errors = new string[sqlEx.Errors.Count];

                            for (int i = 0; i < sqlEx.Errors.Count; i++)
                            {
                                errors[i] = sqlEx.Errors[i].Message;
                            }

                            return errors;
                        default:
                            throw ex;
                    }
                }
            }

            return null;
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
        /// Filters and projects the sequence of values based on a predicate using the provided mapping engine.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A sequence of values matched by function.</returns>
        public IEnumerable<TProjection> Get<TProjection>(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ProjectTo<TProjection>().ToList();
        }

        /// <summary>
        /// Filters and projects the sequence of values based on a predicate using the provided mapping engine.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="page">Page number to take.</param>
        /// <param name="pageSize">Page size to take.</param>
        /// <returns>A sequence of values matched by function.</returns>
        public IEnumerable<TProjection> Get<TProjection>(Expression<Func<TEntity, bool>> predicate, int page, int pageSize)
        {
            return _context.Set<TEntity>().Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ProjectTo<TProjection>().ToList();
        }

        /// <summary>
        /// Asynchronously filters the sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A sequence of values matched by function.</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Include().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Asynchronously filters and projects the sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="page">Page number to take.</param>
        /// <param name="pageSize">Page size to take.</param>
        /// <returns>A sequence of values matched by function.</returns>
        public virtual async Task<PagedResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, int page, int pageSize)
        {
            var query = Include().Where(predicate);
            var count = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResult<TEntity>(items, count, page, count / pageSize + 1, pageSize);
        }

        /// <summary>
        /// Asynchronously filters and projects the sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="page">Page number to take.</param>
        /// <param name="pageSize">Page size to take.</param>
        /// <returns>A sequence of values matched by function.</returns>
        public virtual async Task<PagedResult<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector, bool descending, int page, int pageSize)
        {
            var query = Include().Where(predicate);
            var count = await query.CountAsync();

            if (descending)
            {
                query = query.OrderByDescending(keySelector);
            }
            else
            {
                query = query.OrderBy(keySelector);
            }

            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResult<TEntity>(items, count, page, count / pageSize + 1, pageSize);
        }

        /// <summary>
        /// Asynchronously filters and projects the sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A sequence of values matched by function.</returns>
        public virtual async Task<IEnumerable<TProjection>> GetAsync<TProjection>(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ProjectTo<TProjection>().ToListAsync();
        }

        /// <summary>
        /// Asynchronously filters and projects the sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="page">Page number to take.</param>
        /// <param name="pageSize">Page size to take.</param>
        /// <returns>A sequence of values matched by function.</returns>
        public virtual async Task<IEnumerable<TProjection>> GetAsync<TProjection>(Expression<Func<TEntity, bool>> predicate, int page, int pageSize)
        {
            return await _context.Set<TEntity>().Where(predicate).ProjectTo<TProjection>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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
        /// Returns a projection of the only element of a sequence using the provided mapping engine,
        /// or a default value if the sequence is empty;
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <returns>
        /// The single element of the input sequence, or <see cref="default(TSource)"/> if the sequence
        /// containts no element.
        /// </returns>
        /// <exception cref="ArgumentNullException">source is null.</exception>
        /// <exception cref="InvalidOperationException">source has more than one element.</exception>
        public virtual TProjection SingleOrDefault<TProjection>(Expression<Func<TEntity, bool>> predicate, object parameters = null)
        {
            IQueryable<TProjection> projection;
            var query = _context.Set<TEntity>().Where(predicate);

            if (parameters != null)
            {
                projection = query.ProjectTo<TProjection>(parameters);
            }
            else
            {
                projection = query.ProjectTo<TProjection>();
            }

            return projection.SingleOrDefault();
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
        public virtual async Task<TProjection> SingleOrDefaultAsync<TProjection>(Expression<Func<TEntity, bool>> predicate, object parameters = null)
        {
            IQueryable<TProjection> projection;
            var query = _context.Set<TEntity>().Where(predicate);

            if (parameters != null)
            {
                projection = query.ProjectTo<TProjection>(parameters);
            }
            else
            {
                projection = query.ProjectTo<TProjection>();
            }

            return await projection.SingleOrDefaultAsync();

            //var entity = await _context.Set<TEntity>().Where(predicate).SingleOrDefaultAsync();
            //return Mapper.Map<TEntity, TProjection>(entity);
        }

        public virtual IEnumerable<TEntity> All()
        {
            return Include().ToList();
        }

        public virtual IEnumerable<TProjection> All<TProjection>()
        {
            return _context.Set<TEntity>().ProjectTo<TProjection>().ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await Include().ToListAsync();
        }

        public virtual async Task<IEnumerable<TProjection>> AllAsync<TProjection>()
        {
            return await _context.Set<TEntity>().ProjectTo<TProjection>().ToListAsync();
        }

        /// <summary>
        /// Removes entity from the database.
        /// </summary>
        /// <param name="entity">Entity to remove.</param>
        /// <returns></returns>
        public virtual int Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Removes entity from the database.
        /// </summary>
        /// <param name="entity">Entity to remove.</param>
        /// <returns></returns>
        public virtual async Task<int> RemoveAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a collection of entities from the database.
        /// </summary>
        /// <param name="entities">Collection of entities to remove.</param>
        /// <returns></returns>
        public virtual int RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Removes a collection of entities from the database.
        /// </summary>
        /// <param name="entities">Collection of entities to remove.</param>
        /// <returns></returns>
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

    }
}
