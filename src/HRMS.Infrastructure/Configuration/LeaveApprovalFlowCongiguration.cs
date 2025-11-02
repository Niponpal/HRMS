using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class LeaveApprovalFlowCongiguration
{
    public void Configure(EntityTypeBuilder<LeaveApprovalFlow> builder)
    {
        builder.HasKey(builder => builder.Id);

        builder.ToTable(nameof(LeaveApprovalFlow));

        // Property configurations

        builder.Property(d => d.Stage)
              .IsRequired()
              .HasMaxLength(100);

        builder.Property(d => d.SequenceNo)
               .IsRequired();

        builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(1000);
    }
}
