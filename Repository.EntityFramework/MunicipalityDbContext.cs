using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Threading;
using System.Threading.Tasks;
using Repositories.EntityFramework.Configuration;

using Microsoft.AspNetCore.Identity;

namespace Repositories.EntityFramework
{
  public class MunicipalityDbContext : CustomIdentityDbContext
  {

    public DbSet<IncidentStatus> IncidentStatuses { get; set; }
    public DbSet<Priority> Priorities { get; set; }

    public DbSet<Incident> Incidents { get; set; }


    public MunicipalityDbContext(DbContextOptions options)
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

      
      builder.ApplyConfiguration(new IncidentStatusConfiguration());
      builder.ApplyConfiguration(new PriorityConfiguration());
      builder.ApplyConfiguration(new IncidentConfiguration());
    }

  }
}
