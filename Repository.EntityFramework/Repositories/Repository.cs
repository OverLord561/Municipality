using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Add(entity);
           
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Set<TEntity>().AddAsync(entity);
        }
       
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            
        }
       
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
        }  
      
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Include().Where(predicate).ToList();
        }
       
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, int page, int pageSize)
        {
            return Include().Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Include().SingleOrDefault(predicate);
        }

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
        public virtual void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Remove(entity);
        }       

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
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

        public async Task<bool> SaveChangesAsync()
        {
            int countAffected = await _context.SaveChangesAsync();

            return countAffected > 0;
        }

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return await Include().Select(selector).ToListAsync();
        }

        public List<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return Include().Select(selector).ToList();
        }

        public async Task<IEnumerable<TResult>> WhereSelectAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
        {
            return await Include().Where(predicate).Select(selector).ToListAsync();
        }

        public async Task<IEnumerable<TResult>> OrderBySelectAsync<TResult, TKey>(Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, TResult>> selector)
        {
            return await Include().OrderBy(keySelector).Select(selector).ToListAsync();
        }

        public IEnumerable<TResult> OrderBySelect<TResult, TKey>(Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, TResult>> selector)
        {
            return Include().OrderBy(keySelector).Select(selector).ToList();
        }

        public async Task<IEnumerable<TResult>> WhereOrderBySelectAsync<TResult, TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, TResult>> selector)
        {
            return await Include().Where(predicate).OrderBy(keySelector).Select(selector).ToListAsync();
        }

    }
}
