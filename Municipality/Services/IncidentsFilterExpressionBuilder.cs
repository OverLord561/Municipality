using Models;
using Municipality.Features.Incidents;
using Repositories.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Municipality.Extensions;

namespace Municipality.Services
{
    public class IncidentsFilterExpressionBuilder : IFilterExpressionBuilder<IncidentsQuery, Incident>
    {
        public Expression<Func<Incident, bool>> BuildWhere(IncidentsQuery query)
        {
            query.StatusName = "New";

            Expression<Func<Incident, bool>> result = null;

            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            if (!string.IsNullOrEmpty(query.StatusName))
            {
                result = result.AndAlso(x => x.IncidentStatus.Name.ToLower() == query.StatusName.ToLower());
            }
            result = result.AndAlso(x => x.Approved == query.IsApproved);

            result = result.AndAlso(x => x.Priority.Name == "Low");


            if (result == null) return x => true;

            return result;

        }
    }
}
