using Models;
using Municipality.Features.Incidents;
using Repositories.EntityFramework.Queries;
using System;
using System.Linq.Expressions;

namespace Municipality.Services
{
    public class IncidentsFilterExpressionBuilder : IFilterExpressionBuilder<IncidentsQuery, Incident>
    {
        public Expression<Func<Incident, bool>> BuildWhere(IncidentsQuery query)
        {
            query.StatusName = "New";

            var areParametersValid = query.AreBaseParametersValid();
            if (!areParametersValid)
            {
                return null;
            }

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
