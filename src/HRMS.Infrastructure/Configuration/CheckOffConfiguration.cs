using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class CheckOffConfiguration:IEntityTypeConfiguration<CheckOff>
{
    public void Configure(EntityTypeBuilder<CheckOff> builder)
    {
        builder.HasKey(c=>c.Id);

        builder.ToTable(nameof(CheckOff));

        builder.Property(c=>c.EmployeeId)
            .IsRequired();

        builder.Property(c=>c.MonthlyDeductibleAmount)

            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(c=>c.EffectiveSalaryCycle)
            .IsRequired();

        builder.Property(c=>c.EndSalaryCycle)
            .IsRequired();
    }

}
