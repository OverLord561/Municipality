using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;


namespace Repositories.EntityFramework.Configuration
{
    internal class PriorityConfiguration : IEntityTypeConfiguration<Priority>
    {
        public void Configure(EntityTypeBuilder<Priority> entity)
        {
            entity.HasKey(x => x.Id)
                .HasName("PK_IncidentPriority_ID");

            entity.Property(x => x.Id)
                .UseSqlServerIdentityColumn()
                .HasColumnName("ID");

            entity.Property(x => x.Name)
                .IsRequired();
            entity.ToTable("Priorities");
        }
    }
}
