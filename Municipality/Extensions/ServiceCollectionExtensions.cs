using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Municipality.Features.Incidents;
using Municipality.Services;
using Municipality.Services.Interfaces;
using Repositories;
using Repositories.EntityFramework.Queries;
using Repositories.EntityFramework.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

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


        public static void ServiceCollectionAddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "API for Municipality",
                    Description = "First release for Municipality API for v1 methods.",
                    TermsOfService = "None",
                });

                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });
        }

    }
}
