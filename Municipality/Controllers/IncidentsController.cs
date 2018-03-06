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
using Municipality.Service;
using Municipality.Services.Interfaces;
using Municipality.Features.Incidents;
using Municipality.ModelBinder;

namespace Municipality.Controllers
{
    [Authorize]
    public class IncidentsController : Controller
    {

        private readonly IIncidentService _incidentService;
        private readonly IIncidentStatusRepository _incidentStatusesRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Manager _manager;

        public IncidentsController(
            IIncidentService incidentService,
            IIncidentStatusRepository incidentStatusesRepository,
            IHostingEnvironment hostingEnvironment,
             UserManager<ApplicationUser> userManager,
             IHttpContextAccessor httpContextAccessor,
             Manager manager
            )
        {
            _incidentService = incidentService;
            _incidentStatusesRepository = incidentStatusesRepository;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _manager = manager;
        }


        [HttpGet("/api/incidents")]
        [Produces("application/json")]
        public async Task<IActionResult> GetIncidents([FromQuery] IncidentsQuery query = null)
        {
            return Json(new { Items = await _incidentService.GetIncidentsAsync(query) });
        }

        [HttpGet("api/incidents/not-approved")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNotApproved()
        {

            return Json(
                            new
                            {
                                Items = await _incidentService.GetNotApprovedAsync()
                            }
                        );

        }




        [HttpPost("api/incidents")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateIncident([ModelBinder(BinderType = typeof(IncidentCreationModelBinder))]IncidentViewModel model)
        {

            //var file = Request.Form.Files[0];
            ////.SaveAs(Server.MapPath("/Content/Images/Uploads/" + fileName));
            StatusCodeResult result = null;
            //try
            //{
            //    if (file != null)
            //    {
            //        // путь к папке Files
            //        string path = "/images/incidents/" + file.FileName;

            //        string lat = Request.Form["lat"].ToString();
            //        string lng = Request.Form["lng"].ToString();


            //        var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            //        Incident _newIncident = new Incident
            //        {
            //            Title = Request.Form["title"],
            //            Description = Request.Form["description"],
            //            Latitude = double.Parse(lat, System.Globalization.CultureInfo.InvariantCulture),
            //            Longitude = double.Parse(lng, System.Globalization.CultureInfo.InvariantCulture),
            //            FilePath = path,
            //            IncidentStatusId = 1,
            //            IncidentStatus = _incidentStatusesRepository.SingleOrDefault(x => x.Id == 1),
            //            UserId = userId,
            //            Adress = Request.Form["adress"],
            //            PriorityId = 1
            //        };
            //        //await _incidentsRepository.AddAsync(_newIncident);

            //        // сохраняем файл в папку Files в каталоге wwwroot
            //        using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + path, FileMode.Create))
            //        {
            //            await file.CopyToAsync(fileStream);
            //        }


            //        await _manager.SendMailToEmployee(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value, _newIncident);

            //        result = Ok();
            //        return await GetIncidents();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    result = NoContent();
            //}


            return await Task.FromResult(result);

        }


        [HttpPut("api/incidents/{id}/approve")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> ApproveIncident(int id, [FromBody]IncidentViewModel incident)
        {
            //var _incident = await _incidentsRepository.SingleOrDefaultAsync(x => x.Id == id);
            //if (_incident != null)
            //{
            //  _incident.Approved = true;
            //  _incident.PriorityId = incident.PriorityId;
            //  _incident.Estimate = incident.Estimate;
            //  _incident.DateOfApprove = DateTime.Now;

            //  await _incidentsRepository.UpdateAsync(_incident);
            //  await _manager.SendMailToEmployee(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value, _incident, _incident.ApplicationUser.Email);

            //  await _incidentsRepository.UpdateAsync(_incident);
            //}
            //else
            //{
            //  return await Task.FromResult(NoContent());
            //}

            return await Task.FromResult(Ok());

        }

        //[HttpDelete("api/incidents/{id}")]
        //[Produces("application/json")]
        //[AllowAnonymous]
        //public async Task<IActionResult> DeleteIncident(int id)
        //{
        //var incident = await _incidentsRepository.SingleOrDefaultAsync(x => x.Id == id);
        //if (incident != null)
        //{
        //  await _incidentsRepository.RemoveAsync(incident);
        //  return await Task.FromResult(Ok());
        //}
        //else
        //{
        //  return await Task.FromResult(NoContent());
        //}
        //}
    }
}