

using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class EmployeeFileCongiguration:IEntityTypeConfiguration<EmployeeFile>
{
    public void Configure(EntityTypeBuilder<EmployeeFile> builder)
    {
        builder.HasKey(builder => builder.Id);
        builder.ToTable(nameof(EmployeeFile));
        builder.Property(d => d.FileType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.FileName)
            .IsRequired()
            .HasMaxLength(250);
        builder.Property(d => d.FilePath)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(d => d.Remarks)
            .IsRequired()
            .HasMaxLength(500);

        // Configure relationship with Employee entity
       
             builder.HasOne(builder => builder.Employee)
            .WithMany(builder => builder.EmployeeFiles)
            .HasForeignKey(builder => builder.EmployeeId);

    }

}
