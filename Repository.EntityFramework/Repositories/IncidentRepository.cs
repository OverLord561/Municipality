
using AzureLogger;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;

namespace Repositories.EntityFramework.Repositories
{
    public class IncidentRepository: LoggableRepository<Incident>, IIncidentRepository
    {
        public IncidentRepository(ApplicationDbContext context, ICosmosLogger logger)
    : base(context, logger)
        {

        }
        protected override IQueryable<Incident> Include()
        {
            return base.Include()
                .Include(x => x.IncidentStatus)
                .Include(x => x.Priority)
                .Include(x => x.ApplicationUser);
        }
    }
}


