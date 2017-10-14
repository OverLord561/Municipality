using System.Collections.Generic;
using System.Collections;

namespace Repositories
{
    public class PagedResult<TEntity> : IEnumerable<TEntity>
    {
        private readonly IEnumerable<TEntity> _items;
        private readonly long _totalCount;
        private readonly long _currentPage;
        private readonly long _pageCount;
        private readonly long _pageSize;

        public IEnumerable<TEntity> Items => _items;

        public long TotalCount => _totalCount;

        public long CurrentPage => _currentPage;

        public long PageCount => _pageCount;

        public long PageSize => _pageSize;

        public PagedResult(IEnumerable<TEntity> items, long totalCount, long currentPage, long pageCount, long pageSize)
        {
            _items = items;
            _totalCount = totalCount;
            _currentPage = currentPage;
            _pageCount = pageCount;
            _pageSize = pageSize;
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
