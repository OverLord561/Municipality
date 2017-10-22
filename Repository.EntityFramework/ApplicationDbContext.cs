using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Threading;
using System.Threading.Tasks;
using Repositories.EntityFramework.Configuration;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;

namespace Repositories.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>, IPersistedGrantDbContext, IConfigurationDbContext
    {
        private readonly ConfigurationStoreOptions _configurationStoreOptions = new ConfigurationStoreOptions();
        private readonly OperationalStoreOptions _operationalStoreOptions = new OperationalStoreOptions();
        
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<IncidentStatus> IncidentStatuses { get; set; }
       
        // Identity server
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<IdentityServer4.EntityFramework.Entities.Client> Clients { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }

        public DbSet<ApiResource> ApiResources { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context.
        /// </param> 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().Property(p => p.Id).UseSqlServerIdentityColumn();

            //Customize the ASP.NET Identity model and override the defaults if needed.
            //For example, you can rename the ASP.NET Identity table names and more.
            //Add your customizations after calling base.OnModelCreating(builder);
            builder.AddConfiguration(new IncidentStatusConfiguration());
            builder.AddConfiguration(new IncidentConfiguration());
                       

            // IdentityServer4 contexts configuration
            builder.ConfigureClientContext(_configurationStoreOptions);
            builder.ConfigureResourcesContext(_configurationStoreOptions);
            builder.ConfigurePersistedGrantContext(_operationalStoreOptions);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync(new CancellationToken());
        }
    }
}
