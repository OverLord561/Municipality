using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Municipality.Features.Incidents;
using Municipality.Services;
using Municipality.Services.Interfaces;
using Repositories;
using Repositories.EntityFramework.Queries;
using Repositories.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserHelper, UserHelperService>();

            services.AddScoped<IIncidentStatusRepository, IncidentStatusRepository>();
            services.AddScoped<IIncidentRepository, IncidentRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IFilterExpressionBuilder<IncidentsQuery, Incident>, IncidentsFilterExpressionBuilder>();
            services.AddScoped<IIncidentFilesRepository, IncidentFilesRepository>();


            services.AddScoped<IIncidentService, IncidentService>();

            return services;
        }

    }
}
