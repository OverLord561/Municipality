using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Municipality.Extensions;
using Municipality.Service;
using Repositories.EntityFramework;
using System;

namespace Municipality
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Thi.s method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDbContextPool<ApplicationDbContext>(options =>
            //    options.UseSqlServer(connectionString: Configuration.GetConnectionString("DefaultConnection"),
            //                         sqlServerOptionsAction: action => action.EnableRetryOnFailure(
            //                             maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));

            services.AddRouting(options =>
            {
                // Improve SEO by stopping duplicate URL's due to case differences or trailing slashes.
                // See http://googlewebmastercentral.blogspot.co.uk/2010/04/to-slash-or-not-to-slash.html
                // All generated URL's should append a trailing slash.
                options.AppendTrailingSlash = true;
                // All generated URL's should be lower-case.
                options.LowercaseUrls = true;
            });

            services.AddIdentity<ApplicationUser, IdentityRole<int>>(config =>
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
                //config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.AddCustomServices();

            services.AddSingleton<Manager>();

            services.AddMvc();

            services.ServiceCollectionAddSwagger();

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

            if (env.IsDevelopment())
            {
                app.ApplicationBuilderUseSwagger();
            }

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
