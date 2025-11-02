using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class ProvidentFundConfiguration:IEntityTypeConfiguration<ProvidentFund>
{
    public void Configure(EntityTypeBuilder<ProvidentFund> builder)
    {
        builder.HasKey(pf => pf.Id);
        builder.ToTable(nameof(ProvidentFund));

        // Configure properties
        builder.Property(d => d.PFEmployeePercentage)
            .IsRequired(false);
        builder.Property(d => d.PFEmployerPercentage)
            .IsRequired(false);
        builder.Property(d => d.EffectiveFrom)
            .IsRequired();
        builder.Property(d => d.Remarks)
            .HasMaxLength(500)
            .IsRequired(false);

        // Configure relationships

        builder.HasOne(d=>d.Employee)
            .WithMany(e=>e.ProvidentFunds)
            .HasForeignKey(d=>d.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}
