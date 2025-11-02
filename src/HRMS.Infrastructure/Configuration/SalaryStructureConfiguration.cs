using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class SalaryStructureConfiguration:IEntityTypeConfiguration<SalaryStructure>
{
    public void Configure(EntityTypeBuilder<SalaryStructure> builder)
    {
        builder.HasKey(builder=> builder.Id);
        builder.ToTable(nameof(SalaryStructure));
        // Configure properties
        builder.Property(builder=> builder.EffectiveFrom)
            .IsRequired();
        builder.Property(builder => builder.BasicSalary)
            .IsRequired();
        builder.Property(builder => builder.HouseRent)
            .IsRequired();
        builder.Property(builder => builder.MedicalAllowance)
            .IsRequired();
        builder.Property(builder => builder.FoodAllowance)
            .IsRequired();
        builder.Property(builder => builder.OtherAllowance)
            .IsRequired();
        // Configure computed column for GrossSalary

        builder.HasOne(builder => builder.Employee)
            .WithMany(d => d.SalaryStructures)
            .HasForeignKey(builder => builder.EmployeeId);
    }

}
