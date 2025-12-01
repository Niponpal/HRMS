using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class CanteenChargeConfiguration : IEntityTypeConfiguration<CanteenCharge>
{
    public void Configure(EntityTypeBuilder<CanteenCharge> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable(nameof(CanteenCharge));

        builder.Property(e => e.EmployeeId)
            .IsRequired();

        builder.Property(a=>a.MonthlyDeductibleAmount)
            .IsRequired()

            .HasColumnType("decimal(18,2)");
        builder.Property(e => e.EffectiveSalaryCycle)
            .IsRequired();

        builder.Property(e => e.EndSalaryCycle)
            .IsRequired();

    }
}
