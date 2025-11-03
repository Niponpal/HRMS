using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class PayrollConfiguration:IEntityTypeConfiguration<Payroll>
{
    public void Configure(EntityTypeBuilder<Payroll> builder)
    {
        builder.HasKey(p => p.Id);  
        builder.ToTable(nameof(Payroll));
        // Configure properties
        builder.Property(builder=> builder.PayrollMonth)
            .IsRequired();
        builder.Property(builder=> builder.PayrollYear)
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
        builder.Property(builder => builder.GrossSalary)
            .IsRequired();
        builder.Property(builder => builder.PFEmployee)
            .IsRequired();
        builder.Property(builder => builder.PFEmployer)
            .IsRequired();
        builder.Property(builder => builder.TaxAmount)
            .IsRequired();
        builder.Property(builder => builder.NetPayable)
            .IsRequired();
        builder.Property(builder => builder.ProcessedDate)
            .IsRequired();
        builder.Property(builder => builder.Status)
            .IsRequired()
            .HasMaxLength(50);

        // Configure relationships
        builder.HasOne(p => p.Employee)
            .WithMany(e => e.Payrolls)
            .HasForeignKey(p => p.EmployeeId);


        builder.HasOne(p => p.SalaryStructure)
            .WithMany(s => s.Payrolls)
            .HasForeignKey(p => p.SalaryStructureId);
   

    }
}
