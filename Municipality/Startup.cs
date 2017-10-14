using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.EntityFramework;
using Repositories.EntityFramework.Repositories;
using Repositories;
using AzureLogger;
using Municipality.Extensions;

namespace Municipality
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                // Password settings
                config.Password.RequiredLength = 6;
                config.Password.RequireDigit = true;
                config.Password.RequiredUniqueChars = 3;
                // Lockout settings
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                config.Lockout.MaxFailedAccessAttempts = 5;
                config.Lockout.AllowedForNewUsers = true;
                // User settings
                config.User.RequireUniqueEmail = true;
                // SignIn settings
                config.SignIn.RequireConfirmedEmail = true;
                config.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIncidentStatusRepository, IncidentStatusRepository>();
            services.AddScoped<IIncidentRepository, IncidentRepository>();

            services.AddMvc();
           


            services.AddSingleton(Configuration.GetSection("AzureStorageTables").Get<LoggerPOCO>());
            services.AddScoped<ICosmosLogger, Logger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
            //if (env.IsDevelopment())
            //{
            //    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //    {
            //        var appContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            //        appContext.Database.EnsureDeleted();
            //        appContext.Database.Migrate();
            //        appContext.EnsureSeedData(env);

            //    }
            //}
        }
    }
}
