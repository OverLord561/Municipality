using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Microsoft.AspNetCore.Authorization;
using Municipality.ViewModels;

namespace Municipality.Controllers
{
    public class IncidentsController : Controller
    {

        private readonly IIncidentRepository _incidentsRepository;


        public IncidentsController(IIncidentRepository incidentsRepository)
        {
            _incidentsRepository = incidentsRepository;
        }


        [HttpGet("api/incidents")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> GetIncidents()
        {
            var items = await _incidentsRepository.AllAsync();
            return Json(
                            new
                            {
                                Items = items.Select(x => x.ToViewModel())
                            }
                        );

        }


    }
}