using Models;
using Repositories.EntityFramework.Models;

namespace Municipality.Features.Incidents
{
    public class IncidentsQuery : Query<Incident>
    {
        public bool IsApproved { get; set; }
        public string StatusName { get; set; }
    }
}
