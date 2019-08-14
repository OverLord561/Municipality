using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories.EntityFramework.Configuration
{
    internal class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> entity)
        {
            entity.HasKey(x => x.Id);           

            entity.Property(x => x.Title).IsRequired();

            entity.HasOne(x => x.IncidentStatus)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.IncidentStatusId)
                .HasConstraintName("FK_Incident_Status_ID")
                .IsRequired();

            entity.HasOne(x => x.User)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_Incident_User_ID")
                .IsRequired();

            entity.HasOne(x => x.Priority)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.PriorityId)
                .HasConstraintName("FK_Incident_Priority_ID")
                .IsRequired();

            entity.HasMany(x => x.AttachedFiles)
                .WithOne(x => x.Incident)
                .HasForeignKey(x => x.IncidentId)
                .HasConstraintName("FK_Files_Incident_ID");

            entity.ToTable("Incidents");
        }
    }
}
