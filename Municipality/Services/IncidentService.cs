using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Models;
using Municipality.Extensions;
using Municipality.Features.Incidents;
using Municipality.Services.Interfaces;
using Municipality.ViewModels;
using Municipality.ViewModels.Enums;
using Repositories;
using Repositories.EntityFramework;
using Repositories.EntityFramework.Models;
using Repositories.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IFilterExpressionBuilder<IncidentsQuery, Incident> _expressionBuilder;
        private readonly IIncidentRepository _incidentsRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly IUserHelper _userHelper;

        public IncidentService(ApplicationDbContext context
            , IFilterExpressionBuilder<IncidentsQuery, Incident> expressionBuilder
            , IIncidentRepository incidentsRepository
            , IHostingEnvironment hostingEnvironment
            , IUserHelper userHelper
        )
        {
            _userHelper = userHelper ?? throw new ArgumentNullException(nameof(userHelper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _incidentsRepository = incidentsRepository ?? throw new ArgumentNullException(nameof(incidentsRepository));
            _expressionBuilder = expressionBuilder ?? throw new ArgumentNullException(nameof(expressionBuilder));
            _hostingEnvironment = hostingEnvironment;
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

             await _incidentsRepository.AddAsync(incident);

            return await _incidentsRepository.SaveChangesAsync();
        }

        public async Task<IPagedResult<IncidentViewModel>> GetIncidentsAsync(IncidentsQuery query)
        {
            try
            {
                var expr = _expressionBuilder.BuildWhere(query);
                if (expr == null)
                {
                    return null;
                }

                var result = await _context.Incidents
                    .Include()
                    .Where(expr)
                    .Select(x => x.ToViewModel())
                    .ToPagedEnumerableAsync(query.Page, query.Size, query.SortBy, query.SortDirection);

                return result;
            }
            catch
            {
                return null;
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
