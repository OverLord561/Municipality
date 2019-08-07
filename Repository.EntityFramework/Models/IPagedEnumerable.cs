using System.Collections.Generic;

namespace Repositories.EntityFramework.Models
{
    public interface IPagedEnumerable<out TEntity> : IEnumerable<TEntity>
    {
        IEnumerable<TEntity> Items { get; }

         PageInfo PageInfo { get; }
    }
}
