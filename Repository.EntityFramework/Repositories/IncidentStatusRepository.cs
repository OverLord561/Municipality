using Models;

namespace Repositories.EntityFramework.Repositories
{


    public class IncidentStatusRepository : Repository<IncidentStatus>, IIncidentStatusRepository
    {
        public IncidentStatusRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }

}

