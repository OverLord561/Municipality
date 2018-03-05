using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public interface IPagedEnumerable<out TEntity> : IEnumerable<TEntity>
    {
        /// <summary>
        /// Gets the collection of current page items.
        /// </summary>
        IEnumerable<TEntity> Items { get; }

        /// <summary>
        /// Gets the total items count.
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        int CurrentPage { get; }

        /// <summary>
        /// Gets the total count of pages.
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Gets the items count per page.
        /// </summary>
        int PageSize { get; }
    }
}
