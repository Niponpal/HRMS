using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Configuration;

public class MembershipPayConfiguration:IEntityTypeConfiguration<MembershipPay>
{
    public void Configure(EntityTypeBuilder<MembershipPay> builder)
    {
        builder.HasKey(m => m.Id);
        builder.ToTable(nameof(MembershipPay));
        builder.Property(m => m.EmployeeId)
            .IsRequired();
        builder.Property(m => m.MonthlyDeductibleAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        builder.Property(m => m.EffectiveSalaryCycle)
            .IsRequired();
        builder.Property(m => m.EndSalaryCycle)
            .IsRequired();
    }
}
