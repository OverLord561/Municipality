using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.EntityFramework.Configuration
{
    internal class IncidentFileConfiguration : IEntityTypeConfiguration<IncidentFile>
    {
        public void Configure(EntityTypeBuilder<IncidentFile> entity)
        {
            entity.HasKey(x => x.Id);          

            entity.Property(x => x.IncidentId)
                  .HasColumnName("IncidentID");

            entity.Property(x => x.UploadedById)
                  .HasColumnName("UploadedByID");

            entity.Property(x => x.Name)
                  .HasMaxLength(255)
                  .IsRequired();

            entity.Property(x => x.ContentType)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(x => x.FilePath)
                  .IsRequired();

            entity.Property(x => x.Date)
                  .HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(x => x.Incident)
                  .WithMany(x => x.AttachedFiles)
                  .HasForeignKey(x => x.IncidentId)
                  .HasConstraintName("FK_IncidentFile_Incident_IncidentID")
                                  .OnDelete(DeleteBehavior.ClientSetNull);


            entity.HasOne(x => x.UploadedBy)
                  .WithMany(x => x.IncidentFiles)
                  .HasForeignKey(x => x.UploadedById)
                  .HasConstraintName("FK_IncidentFile_User_UploadedByID");

            entity.ToTable("IncidentFiles");
        }
    }
}
