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
            var user = new ApplicationUser
            {
                ConcurrencyStamp = "6b3b9b11-8f97-4dc2-bfa0-b80b40ae890d",
                Email = "roman@test.com",
                NormalizedEmail = "ROMAN@TEST.COM",
                UserName = "roman@test.com",
                NormalizedUserName = "ROMAN@TEST.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEH8NJi0ZOAkATkqABxRdwrALqMd10IW35NNnRlvVddLMC3eLb35Hu4v+KDMO0/M9gQ==",
                SecurityStamp = "73a61f09-5ad8-4cd8-bc3a-7ad9d21eb0f3",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                LockoutEnabled = true,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                PhoneNumber = "0953393611",
               
            };


           
            var user2 = new ApplicationUser
            {
                ConcurrencyStamp = "6b3b9b11-8f97-4dc2-bfa0-b80b40ae890d",
                Email = "yurapuk452@gmail.com",
                NormalizedEmail = "YURAPUK452@GMAIL.COM",
                UserName = "yurapuk452@gmail.com",
                NormalizedUserName = "YURAPUK452@GMAIL.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEH8NJi0ZOAkATkqABxRdwrALqMd10IW35NNnRlvVddLMC3eLb35Hu4v+KDMO0/M9gQ==",
                SecurityStamp = "73a61f09-5ad8-4cd8-bc3a-7ad9d21eb0f3",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                LockoutEnabled = true,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                PhoneNumber = "0953393612"

            };


            var @new = new IncidentStatus { Name = "New" };
            var progress = new IncidentStatus { Name = "In Progress" };          
            var closed = new IncidentStatus { Name = "Closed" };


            context.IncidentStatuses.Add(@new);
            context.IncidentStatuses.Add(progress);           
            context.IncidentStatuses.Add(closed);
            context.Users.Add(user);
            context.Users.Add(user2);


            var _priorities = new List<Priority> {
                new Priority{
                    Name = "Zero"
                },
                new Priority{
                    Name = "Low"
                },
                new Priority{
                    Name = "Medium"
                },
                new Priority{
                    Name = "High"
                },
            };
            foreach (var pr in _priorities)
            {
                context.Priorities.Add(pr);
            }

            context.SaveChanges();




            var incidents = new List<Incident>
            {
                new Incident
                {
                    Title="Упало дерево",
                    Description= "Затрудняє рух громадського транспорту",
                    IncidentStatusId = @new.Id,
                    Latitude = 48.462592,
                    Longitude = 35.049769,
                    UserId = user.Id,
                    Adress="проспект Дмитра Яворницького, 42-44, Дніпро́, Дніпропетровська область, Украина",
                    Approved = true,
                    FilePath="/images/incidents/tree.png",
                    PriorityId =_priorities[0].Id,
                    DateOfApprove = DateTime.Now,
                    Estimate = 3

                },
                new Incident
                {
                    Title="Прорвана труба",
                    Description= "Прохід громадян неможливий",
                    IncidentStatusId = progress.Id,
                    Latitude = 48.466126,
                    Longitude = 35.049526,
                    UserId = user.Id,
                    Adress="вулиця Глінки, 11, Дніпро́, Дніпропетровська область, Украина",
                    Approved=true,
                    FilePath="/images/incidents/water.png",
                     PriorityId =_priorities[1].Id,
                    DateOfApprove = DateTime.Now.AddDays(-1),
                    Estimate = 15
                },
                new Incident
                {
                    Title="ДТП",
                    Description= "Тролебус виїхав на обочину",
                    IncidentStatusId = @new.Id,
                    Latitude = 48.469868,
                    Longitude = 35.054096,
                    UserId = user.Id,
                    Adress="вулиця Січеславська Набережна, 35А, Дніпро́, Дніпропетровська область, Украина, 49000",
                    Approved = true,
                    FilePath="/images/incidents/dtp.png",
                     PriorityId =_priorities[2].Id,
                    DateOfApprove = DateTime.Now.AddDays(-2),
                    Estimate = 4
                },
                new Incident
                {
                    Title="Воронка в дорожньому полотні",
                    Description= "Прорив водної магістралі. Машина провалилась.",
                    IncidentStatusId = @new.Id,
                    Latitude = 48.467271,
                    Longitude = 35.040321,
                    UserId = user2.Id,
                    Adress="проспект Дмитра Яворницького, 75-77, Дніпро́, Дніпропетровська область, Украина",
                    Approved = false,
                    FilePath="/images/incidents/hole.png",
                     PriorityId =_priorities[0].Id,
                    DateOfApprove = DateTime.Now,
                    Estimate = 10
                },
                new Incident
                {
                    Title="Пошкоджена лінія електропередач",
                    Description= "Кабель висить над тротуарною доріжкою",
                    IncidentStatusId = closed.Id,
                    Latitude = 48.467271,
                    Longitude = 35.040321,
                    UserId = user2.Id,
                    Adress="проспект Дмитра Яворницького, 75-77, Дніпро́, Дніпропетровська область, Украина",
                    Approved = true,
                    FilePath="/images/incidents/electricity.png",
                     PriorityId =_priorities[3].Id,
                    DateOfApprove = DateTime.Now,
                    Estimate = 16
                }

            };

            foreach (Incident log in incidents) {
                context.Incidents.Add(log);
            }
            context.SaveChanges();

        }

    }
}
