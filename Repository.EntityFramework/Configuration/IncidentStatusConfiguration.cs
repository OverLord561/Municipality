using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;


namespace Repositories.EntityFramework.Configuration
{
    internal class IncidentStatusConfiguration : IEntityTypeConfiguration<IncidentStatus>
    {
        public void Configure(EntityTypeBuilder<IncidentStatus> entity)
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired();
            entity.ToTable("IncidentStatuses");
        }
    }
}
