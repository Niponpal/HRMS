using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class FineConfiguration:IEntityTypeConfiguration<Fine>
{
    public void Configure(EntityTypeBuilder<Fine> builder)
    {
        builder.HasKey(a=>a.Id);
        builder.ToTable(nameof(Fine));

        builder.Property(a=>a.EmployeeId)
  
            .IsRequired();

        builder.Property(a=>a.MonthlyDeductibleAmount)
            .IsRequired()
            .IsRequired();

        builder.Property(a=>a.EffectiveSalaryCycle)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a=>a.EndSalaryCycle)
            .IsRequired();
    }
}
