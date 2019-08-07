using System.Collections.Generic;

namespace Repositories.EntityFramework.Models
{
    public interface IPagedResult<out TEntity>
    {
        IEnumerable<TEntity> Items { get; }

        PageInfo PageInfo { get; }
    }
}
