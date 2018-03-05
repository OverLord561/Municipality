using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Repositories.EntityFramework.Queries
{
    public interface IFilterExpressionBuilder<in TQuery, T> where TQuery : IQuery<T>
                                                             where T : class, new()
    {
        Expression<Func<T, bool>> BuildWhere(TQuery query);

    }
}
