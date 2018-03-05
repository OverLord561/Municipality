using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EntityFramework.Queries
{
    public static class IQueryableExtensions
    {
        public static async Task<IPagedEnumerable<T>> ToPagedEnumerableAsync<T>(this IQueryable<T> source, int page, int size) where T : class, new()
        {
            var count = await source.CountAsync();
            source = source.Skip((page - 1) * size).Take(size);
            var items = await source.ToListAsync();
            return new PagedResult<T>(items, count, page, count / size + 1, size);
        }

        public static IPagedEnumerable<T> ToPagedEnumerable<T>(this IEnumerable<T> source, int page, int size, int count) where T : class, new()
        {
            return new PagedResult<T>(source, count, page, count / size + 1, size);
        }
    }
}
