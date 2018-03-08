using Models;
using Municipality.Services.Interfaces;
using Municipality.ViewModels;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Municipality.Extensions;
using Municipality.Features.Incidents;
using Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Repositories.EntityFramework.Queries;
using Municipality.ViewModels.Enums;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

namespace Municipality.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IUserHelper _userHelper;
        private readonly IIncidentRepository _incidentsRepository;
        private readonly ApplicationDbContext _context;
        private readonly IFilterExpressionBuilder<IncidentsQuery, Incident> _expressionBuilder;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IIncidentFilesRepository _incidentFilesRepository;





        public IncidentService(ApplicationDbContext context, IIncidentRepository incidentsRepository,
                        IFilterExpressionBuilder<IncidentsQuery, Incident> expressionBuilder,
                                    IHostingEnvironment hostingEnvironment, IUserHelper userHelper, IIncidentFilesRepository incidentFilesRepository



            )
        {
            _userHelper = userHelper ?? throw new ArgumentNullException(nameof(userHelper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _incidentsRepository = incidentsRepository ?? throw new ArgumentNullException(nameof(incidentsRepository));
            _expressionBuilder = expressionBuilder ?? throw new ArgumentNullException(nameof(expressionBuilder));
            _hostingEnvironment = hostingEnvironment;
            _incidentFilesRepository = incidentFilesRepository ?? throw new ArgumentNullException(nameof(incidentFilesRepository));



        }

        public async Task<bool> CreateIncident(IncidentViewModel model)
        {
            Incident incident = new Incident();
            incident = IncidentViewModelToIncident(incident, model);
            if (model.AttachedFiles != null && model.AttachedFiles.Count > 0)
            {
                incident.AttachedFiles = new List<IncidentFile>(model.AttachedFiles.Count);

                foreach (var attachedFile in model.AttachedFiles)
                {
                    attachedFile.Name = $"{incident.Title}_{attachedFile.Name}";

                    string path = "/images/incidents/" + attachedFile.Name;
                    using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + path, FileMode.Create))
                    {

                        await attachedFile.FormFile.CopyToAsync(fileStream);


                        incident.AttachedFiles.Add(new IncidentFile
                        {
                            IncidentId = incident.Id,
                            Name = attachedFile.Name,
                            ContentType = attachedFile.ContentType,
                            FilePath = path,
                            UploadedById = _userHelper.GetUserId()
                        });
                    }
                }
            }

            int i = await _incidentsRepository.AddAsync(incident);


            return i > 0;

        }

        public async Task<IPagedEnumerable<IncidentViewModel>> GetIncidentsAsync(IncidentsQuery query)
        {
            try
            {
                return await _context.Incidents
                    .Include()
                    .Where(_expressionBuilder.BuildWhere(query))
                    .Select(x => x.ToViewModel())
                    .ToPagedEnumerableAsync(query.Page, query.Size);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task<IEnumerable<Incident>> GetNotApprovedAsync()
        {
            return await _incidentsRepository.GetAsync(x => x.Approved == false);
        }


        private Incident IncidentViewModelToIncident(Incident incident, IncidentViewModel model)
        {

            incident.IncidentStatusId = (int)IncidentStatusesEnum.New; // 1 - New
            incident.Title = model.Title;
            incident.Description = model.Description;
            incident.Adress = model.Adress;
            incident.Latitude = model.Lat;
            incident.Longitude = model.Lng;
            incident.PriorityId = (int)IncidentPrioritiesEnum.Zero;
            incident.UserId = _userHelper.GetUserId();


            return incident;
        }

    }
}
