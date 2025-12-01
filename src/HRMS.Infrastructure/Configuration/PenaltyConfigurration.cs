using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class PenaltyConfigurration:IEntityTypeConfiguration<Penalty>
{
    public void Configure(EntityTypeBuilder<Penalty> builder)
    {
           builder.HasKey(p=>p.Id);
           builder.ToTable(nameof(Penalty));

           builder.Property(p => p.EmployeeId)
            .IsRequired();

        builder.Property(p => p.MonthlyDeductibleAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.EffectiveSalaryCycle)
            .IsRequired();

        builder.Property(a=>a.EndSalaryCycle)

            .IsRequired();
    }
}
