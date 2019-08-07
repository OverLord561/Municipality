using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;

namespace Repositories.EntityFramework.Repositories
{
    public class IncidentRepository : Repository<Incident>, IIncidentRepository
    {
        public IncidentRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        protected override IQueryable<Incident> Include()
        {
            return base.Include()
                .Include(x => x.IncidentStatus)
                .Include(x => x.Priority)
                .Include(x => x.User);
        }
    }
}


