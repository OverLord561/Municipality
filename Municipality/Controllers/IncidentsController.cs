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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Municipality.Controllers
{
    public class IncidentsController : Controller
    {

        private readonly IIncidentRepository _incidentsRepository;
        private readonly IIncidentStatusRepository _incidentStatusesRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IncidentsController(
            IIncidentRepository incidentsRepository,
            IIncidentStatusRepository incidentStatusesRepository,
            IHostingEnvironment hostingEnvironment,
             UserManager<ApplicationUser> userManager,
             IHttpContextAccessor httpContextAccessor
            )
        {
            _incidentsRepository = incidentsRepository;
            _incidentStatusesRepository = incidentStatusesRepository;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
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
        public async Task<IActionResult> CreateIncident(ICollection<IFormFile> files)
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

                  
                    var userId = Convert.ToInt32( _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                    await _incidentsRepository.AddAsync(new Incident
                    {
                        Title = Request.Form["title"],
                        Description = Request.Form["description"],
                        Latitude = double.Parse(lat, System.Globalization.CultureInfo.InvariantCulture),
                        Longitude = double.Parse(lng, System.Globalization.CultureInfo.InvariantCulture),
                        FilePath = path,
                        IncidentStatusId = 1,
                        IncidentStatus = _incidentStatusesRepository.SingleOrDefault(x => x.Id == 1),
                        UserId = userId,
                        Adress = Request.Form["adress"]
                    });

                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    result = Ok();
                    return await GetIncidents();
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