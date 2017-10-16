using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Microsoft.AspNetCore.Authorization;
using Municipality.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Models;

namespace Municipality.Controllers
{
    public class IncidentsController : Controller
    {

        private readonly IIncidentRepository _incidentsRepository;
        private readonly IIncidentStatusRepository _incidentStatusesRepository;
        private readonly IHostingEnvironment _hostingEnvironment;


        public IncidentsController(IIncidentRepository incidentsRepository, IIncidentStatusRepository incidentStatusesRepository, IHostingEnvironment hostingEnvironment)
        {
            _incidentsRepository = incidentsRepository;
            _incidentStatusesRepository = incidentStatusesRepository;
            _hostingEnvironment = hostingEnvironment;
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


        [HttpPost("api/incident")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateIncident()
        {
            var file = Request.Form.Files[0];

            //.SaveAs(Server.MapPath("/Content/Images/Uploads/" + fileName));
            StatusCodeResult result = null;
            try
            {
                if (file != null)
                {
                    // путь к папке Files
                    string path = "/images/incidents/" + file.FileName;

                    string lat = Request.Form["lat"].ToString();
                    string lng = Request.Form["lng"].ToString();

                                       
                   
                    await _incidentsRepository.AddAsync(new Incident
                    {
                        Title = Request.Form["title"],
                        Description = Request.Form["description"],
                        Latitude = double.Parse(lat, System.Globalization.CultureInfo.InvariantCulture),
                        Longitude = double.Parse(lng, System.Globalization.CultureInfo.InvariantCulture),
                        FilePath = path,
                        IncidentStatusId = 1,
                        IncidentStatus = _incidentStatusesRepository.SingleOrDefault(x => x.Id == 1)
                    });

                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    result = Ok();
                }
            }
            catch (Exception ex)
            {
                result = NoContent();
            }


            return await Task.FromResult(result);           

        }


    }
}