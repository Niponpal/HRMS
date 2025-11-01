using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class LeaveTypeConfiguration:IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.HasKey(lt => lt.Id);
        builder.ToTable("LeaveTypes");

        // Configure properties
        builder.Property(d=>d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d=>d.MaxDaysPerYear)
            .IsRequired();

        builder.Property(d=>d.IsPaid)
            .IsRequired();

        // Configure relationships

        builder.HasMany(d => d.LeaveAllocations)
            .WithOne(d =>d.LeaveType)
            .HasForeignKey(d =>d.LeaveTypeId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
