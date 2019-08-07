using Repositories.EntityFramework.Models;
using System;
using System.Linq.Expressions;

namespace Repositories.EntityFramework.Queries
{
    public interface IFilterExpressionBuilder<in TQuery, T> where TQuery : IQuery<T>
                                                             where T : class, new()
    {
        Expression<Func<T, bool>> BuildWhere(TQuery query);

    }
}
