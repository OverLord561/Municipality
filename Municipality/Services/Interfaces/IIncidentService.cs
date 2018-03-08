using Models;
using Municipality.Features.Incidents;
using Municipality.ViewModels;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Services.Interfaces
{
    public interface IIncidentService
    {
        Task<IPagedEnumerable<IncidentViewModel>> GetIncidentsAsync(IncidentsQuery query);
        Task<IEnumerable<Incident>> GetNotApprovedAsync();
        Task<bool> CreateIncident(IncidentViewModel incident);
        
    }
}
