using Models;
using Municipality.Features.Incidents;
using Municipality.ViewModels;
using Repositories.EntityFramework.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Municipality.Services.Interfaces
{
    public interface IIncidentService
    {
        Task<IPagedResult<IncidentViewModel>> GetIncidentsAsync(IncidentsQuery query);
        Task<IEnumerable<Incident>> GetNotApprovedAsync();
        Task<bool> CreateIncident(IncidentViewModel incident);
        
    }
}
