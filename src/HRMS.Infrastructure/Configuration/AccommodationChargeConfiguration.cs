using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Configuration;

public class AccommodationChargeConfiguration:IEntityTypeConfiguration<AccommodationCharge>
{
    public void Configure(EntityTypeBuilder<AccommodationCharge> builder)
    {
        builder.HasKey(a=>a.Id);
        builder.ToTable(nameof(AccommodationCharge));

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
