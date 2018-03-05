using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;


namespace Repositories.EntityFramework.Configuration
{
    internal class IncidentStatusConfiguration : IEntityTypeConfiguration<IncidentStatus>
    {
        public void Configure(EntityTypeBuilder<IncidentStatus> entity)
        {
            entity.HasKey(x => x.Id)
                .HasName("PK_IncidentStatus_ID");

            entity.Property(x => x.Id)
                .UseSqlServerIdentityColumn()
                .HasColumnName("ID");

            entity.Property(x => x.Name)
                .IsRequired();
            entity.ToTable("IncidentStatuses");
        }
    }
}
