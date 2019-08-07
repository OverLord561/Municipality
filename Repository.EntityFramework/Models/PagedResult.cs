using System.Collections.Generic;

namespace Repositories.EntityFramework.Models
{
    public class PagedResult<TEntity> : IPagedResult<TEntity>
    {

        public IEnumerable<TEntity> Items { get; }

        public PageInfo PageInfo { get; }


        public PagedResult(IEnumerable<TEntity> items, PageInfo pageInfo)
        {
            Items = items;
            PageInfo = pageInfo;
        }
    }
}
