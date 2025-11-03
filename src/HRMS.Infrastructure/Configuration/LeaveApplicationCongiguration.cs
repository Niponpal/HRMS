using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class LeaveApplicationCongiguration:IEntityTypeConfiguration<LeaveApplication>
{
    public void Configure(EntityTypeBuilder<LeaveApplication> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable(nameof(LeaveApplication));

        // Property configurations
        builder.Property(e => e.Reason)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(e => e.ImagePath)
                .IsRequired(false)
               .HasMaxLength(500);

        // Relationship configurations

        builder.HasOne(d => d.Employee)
            .WithMany(p => p.LeaveApplications)
            .HasForeignKey(d => d.EmployeeId);


        // Relationship configurations
        builder.HasOne(d => d.LeaveType)
            .WithMany(p => p.LeaveAllocations)
            .HasForeignKey(d => d.LeaveTypeId);


    }
}
