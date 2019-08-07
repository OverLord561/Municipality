using System.Collections;
using System.Collections.Generic;

namespace Repositories.EntityFramework.Models
{
    public class PagedResult<TEntity> : IPagedEnumerable<TEntity>
    {
        private readonly IEnumerable<TEntity> _items;
        private readonly PageInfo _pageInfo;

        public IEnumerable<TEntity> Items => _items;

        public PageInfo PageInfo => _pageInfo;


        public PagedResult(IEnumerable<TEntity> items, PageInfo pageInfo)
        {
            _items = items;
            _pageInfo = pageInfo;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
