using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Repositories.EntityFramework
{
    public class PagedResult<TEntity> : IPagedEnumerable<TEntity>
    {
        private readonly IEnumerable<TEntity> _items;
        private readonly int _totalCount;
        private readonly int _currentPage;
        private readonly int _pageCount;
        private readonly int _pageSize;

        /// <summary>
        /// Gets the collection of current page items.
        /// </summary>
        public IEnumerable<TEntity> Items => _items;

        /// <summary>
        /// Gets the total items count.
        /// </summary>
        public int TotalCount => _totalCount;

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        public int CurrentPage => _currentPage;

        /// <summary>
        /// Gets the total count of pages.
        /// </summary>
        public int PageCount => _pageCount;

        /// <summary>
        /// Gets the items count per page.
        /// </summary>
        public int PageSize => _pageSize;

        public PagedResult(IEnumerable<TEntity> items, int totalCount, int currentPage, int pageCount, int pageSize)
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
