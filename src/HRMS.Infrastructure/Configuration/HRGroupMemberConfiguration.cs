using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class HRGroupMemberConfiguration:IEntityTypeConfiguration<HRGroupMember>
{
    public void Configure(EntityTypeBuilder<HRGroupMember> builder)
    {
        builder.HasKey(builder => builder.Id);
        builder.ToTable(nameof(HRGroupMember));
        // Relationship configurations

        builder.HasOne(builder => builder.Employee)
            .WithMany(employee => employee.HRGroupMembers)
            .HasForeignKey(builder => builder.EmployeeId);

            }
}
