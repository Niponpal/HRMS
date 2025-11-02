using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class LeaveApprovalConfiguration:IEntityTypeConfiguration<LeaveApproval>
{
    public void Configure(EntityTypeBuilder<LeaveApproval> builder)
    {
        builder.HasKey(builder => builder.Id);

        builder.ToTable(nameof(LeaveApproval));

        // Property configurations  
        builder.Property(d=>d.Stage)
              .IsRequired()
              .HasMaxLength(100);

        builder.Property(d=>d.ActionStatus)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(d=>d.Remarks)
                .IsRequired()
                .HasMaxLength(1000);

        // Relationship configurations
        builder.HasOne(d=>d.LeaveApplication)
            .WithMany(p=>p.LeaveApprovals)
            .HasForeignKey(d=>d.LeaveId);
    }
}
