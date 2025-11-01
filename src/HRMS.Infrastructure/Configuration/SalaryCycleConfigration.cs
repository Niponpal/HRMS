using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class SalaryCycleConfigration:IEntityTypeConfiguration<SalaryCycle>
{
    public void Configure(EntityTypeBuilder<SalaryCycle> builder)
    {
        builder.HasKey(sc => sc.Id);
        builder.ToTable(nameof(SalaryCycle));

        // Configure properties

        builder.Property(sc => sc.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(sc => sc.StartDate)
            .IsRequired();

        builder.Property(sc => sc.EndDate)
            .IsRequired();

        builder.Property(sc => sc.NumberOfDays)
            .IsRequired();

        //  add A Relationships if any
    }
}
