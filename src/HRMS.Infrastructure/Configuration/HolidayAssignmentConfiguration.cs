using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class HolidayAssignmentConfiguration:IEntityTypeConfiguration<HolidayAssignment>
{
    public void Configure(EntityTypeBuilder<HolidayAssignment> builder)
    {
        builder.HasKey(builder => builder.Id);
        builder.ToTable(nameof(HolidayAssignment));

        // Relationship configurations
        builder.HasOne(ha => ha.Holiday)
                 .WithMany(h => h.HolidayAssignments)
                 .HasForeignKey(ha => ha.HolidayId);

        // Relationship configurations
        builder.HasOne(ha => ha.Department)
               .WithMany(d => d.HolidayAssigment)
               .HasForeignKey(ha => ha.DepartmentId);

        // Relationship configurations

        builder.HasOne(ha => ha.Shift)
                 .WithMany(s => s.HolidayAssignments)
                 .HasForeignKey(ha => ha.ShiftId)
                 .OnDelete(DeleteBehavior.Restrict);
    }

}
