using Models;
using AzureLogger;

namespace Repositories.EntityFramework.Repositories
{
    

    public class IncidentStatusRepository : LoggableRepository<IncidentStatus>, IIncidentStatusRepository
    {
        public IncidentStatusRepository(ApplicationDbContext context, ICosmosLogger logger)
    : base(context, logger)
        {
        }
    }

}

