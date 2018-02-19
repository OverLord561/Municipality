﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Services.Interfaces
{
  public interface IIncidentService
  {
    Task<IEnumerable<Incident>> GetActiveIncidentsAsync();
    Task<IEnumerable<Incident>> GetNotApprovedAsync();
  }
}
