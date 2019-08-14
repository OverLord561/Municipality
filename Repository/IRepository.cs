using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository<TEntity> where TEntity:class, new()
    {
        long Count { get; }

        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);


        void Update(TEntity entity);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        (IEnumerable<TEntity>,int) Get(int page, int size);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, int page, int pageSize);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
       
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> All();
        
        Task<IEnumerable<TEntity>> AllAsync();

        void Remove(TEntity entity);
               
        void RemoveRange(IEnumerable<TEntity> entities);
               
        void DetachAllEntities();

        Task<TResult> Max<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null) where TResult : struct;

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> SaveChangesAsync();

        Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TEntity, TResult>> predicate);

        List<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> predicate);

        Task<IEnumerable<TResult>> WhereSelectAsync<TResult>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector);

        Task<IEnumerable<TResult>> OrderBySelectAsync<TResult, TKey>(Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, TResult>> selector);

        IEnumerable<TResult> OrderBySelect<TResult, TKey>(Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, TResult>> selector);

        Task<IEnumerable<TResult>> WhereOrderBySelectAsync<TResult, TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector,
             Expression<Func<TEntity, TResult>> selector);
    }
}
