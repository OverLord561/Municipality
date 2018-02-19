using Models;
using Municipality.Services.Interfaces;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Services
{
  public class IncidentService : IIncidentService
  {
    private readonly IIncidentRepository _incidentsRepository;

    public IncidentService(IIncidentRepository incidentsRepository)
    {
      _incidentsRepository = incidentsRepository ?? throw new ArgumentNullException(nameof(incidentsRepository));

    }

    public async Task<IEnumerable<Incident>> GetActiveIncidentsAsync()
    {
      return await _incidentsRepository.GetAsync(x => x.IncidentStatus.Name != "Closed" && x.Approved == true);
    }

    public async Task<IEnumerable<Incident>> GetNotApprovedAsync()
    {
      return await _incidentsRepository.GetAsync(x => x.Approved == false);
    }
  }
}
