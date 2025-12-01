using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class TransportServiceChargeConfiguration:IEntityTypeConfiguration<TransportServiceCharge>
{
    public void Configure(EntityTypeBuilder<TransportServiceCharge> builder)
    {
        builder.HasKey(t=>t.Id);
        builder.ToTable(nameof(TransportServiceCharge));
        builder.Property(t=>t.EmployeeId)
            .IsRequired();
        builder.Property(t=>t.MonthlyDeductibleAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        builder.Property(t=>t.EffectiveSalaryCycle)
            .IsRequired();
        builder.Property(t=>t.EndSalaryCycle)
            .IsRequired();
    }
}
