using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories.EntityFramework.Configuration
{
    internal class IncidentConfiguration : DbEntityConfiguration<Incident>
    {
        public override void Configure(EntityTypeBuilder<Incident> entity)
        {
            entity.HasKey(x => x.Id).HasName("PK_Incident_ID");

            entity.Property(x => x.Id)
                .UseSqlServerIdentityColumn()
                .HasColumnName("ID");

            entity.Property(x => x.Title).IsRequired();

            entity.HasOne(x => x.IncidentStatus)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.IncidentStatusId)
                .HasConstraintName("FK_Incident_Status_ID")
                .IsRequired();

            entity.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_Incident_User_ID")
                .IsRequired();

            entity.ToTable("Incident");
        }
    }
}
