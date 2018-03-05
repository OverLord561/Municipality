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

namespace Municipality.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentsRepository;
        private readonly ApplicationDbContext _context;
        private readonly IFilterExpressionBuilder<IncidentsQuery, Incident> _expressionBuilder;



        public IncidentService(ApplicationDbContext context, IIncidentRepository incidentsRepository,
                        IFilterExpressionBuilder<IncidentsQuery, Incident> expressionBuilder

            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _incidentsRepository = incidentsRepository ?? throw new ArgumentNullException(nameof(incidentsRepository));
            _expressionBuilder = expressionBuilder ?? throw new ArgumentNullException(nameof(expressionBuilder));

        }

        public async Task<IEnumerable<IncidentViewModel>> GetActiveIncidentsAsync(IncidentsQuery query)
        {
            try
            {
                return await _context.Incidents
                    .Include()
                    .Where(_expressionBuilder.BuildWhere(query))
                    .Select(x=>x.ToViewModel())
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
    }
}
