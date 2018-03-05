using Models;
using Municipality.ViewModels;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Features.Incidents
{
    public class IncidentsQuery : Query<Incident>
    {
        public bool IsApproved { get; set; }
        public string StatusName { get; set; }
    }
}
