using Microsoft.EntityFrameworkCore;
using Repositories.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories.EntityFramework.Queries
{
    public static class QueryableExtensions
    {
        // if you have to sort string values but original are decimal, datetime and etc
        // key is property from SortBy
        // value is field in ViewModel
        private static readonly Dictionary<string, string> _sortingDictionary = new Dictionary<string, string>()
        {
            { "amount", "OriginalAmount"},
            { "transactiondatetime","OriginalTransactionDateTime"},
        };

        public static async Task<IPagedResult<T>> ToPagedEnumerableAsync<T>(this IQueryable<T> source, int page, int size, string sortBy, string sortDirection) where T : class, new()
        {
            var isSortByValid = !string.IsNullOrEmpty(sortBy) ? source.IsSortByValid(sortBy) : true;
            if (!isSortByValid)
            {
                return null;
            }

            var items = await source.ToListAsync();
            var count = items.Count;

            if (!string.IsNullOrEmpty(sortBy))
            {
                var sorted = items.AsQueryable().OrderBy(sortBy, sortDirection);
                // IF COLUMN EXISTS IN VIEWMODEL
                if (sorted != null)
                {
                    items = sorted.Skip((page - 1) * size).Take(size).ToList();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                items = items.Skip((page - 1) * size).Take(size).ToList();
            }

            var pageInfo = new PageInfo
            {
                TotalPage = count == size ? 1 :
                    count % size == 0 ?
                        count / size : count / size + 1,
                TotalRecord = count

            };
            return new PagedResult<T>(items, pageInfo);
        }

        public static IPagedResult<T> ToPagedEnumerable<T>(this IEnumerable<T> source, int page, int size, string sortBy, string sortDirection) where T : class, new()
        {
            var isSortByValid = !string.IsNullOrEmpty(sortBy) ? source.AsQueryable().IsSortByValid(sortBy) : true;
            if (!isSortByValid)
            {
                return null;
            }

            var items = source;
            var count = items.Count();

            if (!string.IsNullOrEmpty(sortBy))
            {
                var sorted = items.AsQueryable().OrderBy(sortBy, sortDirection);
                // IF COLUMN EXISTS IN VIEWMODEL
                if (sorted != null)
                {
                    items = sorted.Skip((page - 1) * size).Take(size).ToList();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                items = items.Skip((page - 1) * size).Take(size).ToList();
            }

            var pageInfo = new PageInfo
            {
                TotalPage = count == size ? 1 :
                    count % size == 0 ?
                        count / size : count / size + 1,
                TotalRecord = count

            };
            return new PagedResult<T>(items, pageInfo);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> q, string columnName, string direction)
        {
            try
            {
                // In View model amount is "1.00" string
                // to sort strings there is helper original amount property 1,00 with decimal type
                // and with this additional step decimal will be sorted
                if (_sortingDictionary.ContainsKey(columnName.ToLower()))
                {
                    _sortingDictionary.TryGetValue(columnName.ToLower(), out var helperColumnName);
                    columnName = helperColumnName;
                }

                var param = Expression.Parameter(typeof(T), "p");
                var prop = Expression.Property(param, columnName);
                var exp = Expression.Lambda(prop, param);

                string method = direction == SortDirectionEnum.ASC.ToString() ? "OrderBy" : "OrderByDescending";

                Type[] types = new Type[] { q.ElementType, exp.Body.Type };
                var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);

                return q.Provider.CreateQuery<T>(mce);
            }
            catch
            {
                return null;
            }
        }

        public static Type GetTypeOfItemInCollection<T>(this IQueryable<T> q)
        {
            return typeof(T);
        }

        public static bool IsSortByValid<T>(this IQueryable<T> source, string sortBy)
        {
            var type = source.GetTypeOfItemInCollection();
            var properties = type.GetProperties();

            return properties.Select(x => x.Name.ToLower()).Contains(sortBy.ToLower());

        }
    }
}
