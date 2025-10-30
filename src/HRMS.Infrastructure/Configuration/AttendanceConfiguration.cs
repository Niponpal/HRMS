using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class AttendanceConfiguration:IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder) 
    {
        builder.HasKey(h=> h.Id);
        builder.ToTable(nameof(Attendance));
       
        builder.Property(a => a.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a=> a.Remarks)
            .HasMaxLength(500)
            .IsRequired(false);


        // Configure relationship with Employee entity

        builder.HasOne(a => a.Employee)
            .WithMany(e => e.Attendances)
            .HasForeignKey(a => a.EmployeeId);

    }
}