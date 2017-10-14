using Microsoft.AspNetCore.Hosting;
using Models;
using Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Extensions
{
    public static class ApplicationDbContextExtensions
    {

        public static void EnsureSeedData(this ApplicationDbContext context, IHostingEnvironment env)
        {   

            var @new = new IncidentStatus { Name = "New" };
            var progress = new IncidentStatus { Name = "In Progress" };          
            var closed = new IncidentStatus { Name = "Closed" };


            context.IncidentStatuses.Add(@new);
            context.IncidentStatuses.Add(progress);           
            context.IncidentStatuses.Add(closed);
            context.SaveChanges();

            var incidents = new List<Incident>
            {
                new Incident
                {                    
                    Title="Упало дерево",
                    Description= "Затрудняє рух громадського транспорту",
                    IncidentStatusId = @new.Id,
                    IncidentStatus= @new,
                    Longitude = 48.462592,
                    Latitude = 35.049769

                },
                new Incident
                {
                    Title="Прорвана труба",
                    Description= "Прохід громадян неможливий",
                    IncidentStatusId = progress.Id,
                    IncidentStatus= progress,
                    Longitude = 48.466126,
                    Latitude = 35.049526
                },
                new Incident
                {
                    Title="ДТП",
                    Description= "Тролебус виїхав на обочину",
                    IncidentStatusId = closed.Id,
                    IncidentStatus= closed,
                    Longitude = 48.469868,
                    Latitude = 35.054096
                },
                new Incident
                {
                    Title="Воронка в дорожньому полотні",
                    Description= "Прорив водної магістралі. Машина провалилась.",
                    IncidentStatusId = @new.Id,
                    IncidentStatus= @new,
                    Longitude = 48.467271,
                    Latitude = 35.040321
                }
                
            };

            foreach (Incident log in incidents) {
                context.Add(log);
            }
            context.SaveChanges();

        }

    }
}
