using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class FinancialYearConfiguration:IEntityTypeConfiguration<FinancialYear>
{
    public void Configure(EntityTypeBuilder<FinancialYear> builder)
    {
        builder.HasKey(builder=> builder.Id);
        builder.ToTable(nameof(FinancialYear));
        // Configure properties

        builder.Property(builder=> builder.YearName)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(builder=> builder.FromDate)
            .IsRequired();

        builder.Property(builder=> builder.ToDate)
            .IsRequired();

        // add A Relationships if any
    }
}
