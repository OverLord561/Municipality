using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Incident> Include(this IQueryable<Incident> queryable)
        {
            return queryable.Include(x => x.IncidentStatus)
                .Include(x=>x.Priority);
        }
    }
}
