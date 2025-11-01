using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration
{
    public class TaxSettingConfiguration:IEntityTypeConfiguration<TaxSetting>
    {
        public void Configure(EntityTypeBuilder<TaxSetting> builder)
        {
            builder.HasKey(d=>d.Id);
            builder.ToTable(nameof(TaxSetting));
            // Properties Configuration
            builder.Property(d=>d.TaxYear)
                .IsRequired();
            builder.Property(d => d.MinIncome)
                .IsRequired();
            builder.Property(d => d.MaxIncome)
                .IsRequired();
            builder.Property(d => d.TaxPercentage)
                .IsRequired();



        }
    }
}
