using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class ArrearConfiguration:IEntityTypeConfiguration<Arrear>
{
    public void Configure(EntityTypeBuilder<Arrear> builder)
    {
        builder.HasKey(a => a.Id);
        builder.ToTable(nameof(Arrear));

        builder.Property(a=>a.EmployeeId)
            .IsRequired();

        builder.Property(a=>a.MonthlyDeductibleAmount)

            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(a=>a.EffectiveSalaryCycle)
            .IsRequired();

        builder.Property(a=>a.EndSalaryCycle)
            .IsRequired();

    }
}
