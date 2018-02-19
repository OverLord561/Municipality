using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.EntityFramework
{
  public abstract class CustomIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
  {
    public CustomIdentityDbContext(DbContextOptions options)
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

      //Customize the ASP.NET Identity model and override the defaults if needed.
      //For example, you can rename the ASP.NET Identity table names and more.
      //Add your customizations after calling base.OnModelCreating(builder);

      builder.Entity<ApplicationUser>()
          .Property(p => p.Id)
          .UseSqlServerIdentityColumn();

      builder.Entity<ApplicationUser>()
          .HasMany(e => e.Claims)
          .WithOne()
          .HasForeignKey(e => e.UserId)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);

      builder.Entity<ApplicationUser>()
          .HasMany(e => e.Logins)
          .WithOne(x => x.User)
          .HasForeignKey(e => e.UserId)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);

      builder.Entity<ApplicationUser>()
          .HasMany(e => e.Roles)
          .WithOne()
          .HasForeignKey(e => e.UserId)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
