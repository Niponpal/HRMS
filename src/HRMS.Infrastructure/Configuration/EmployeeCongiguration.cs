using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class EmployeeCongiguration:IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(builder => builder.Id);
        builder.ToTable(nameof(Employee));
        // Properties Configuration
        builder.Property(d=>d.EmployeeCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d=>d.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d=>d.Gender)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(d=>d.NationalId)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(d=>d.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d=>d.Phone)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(d=>d.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d=>d.PermanentAddress)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d=>d.MaritalStatus)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(d=>d.BloodGroup)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(d=>d.EmergencyContactName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d=>d.EmergencyContactPhone)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(d=>d.IsActive)
            .IsRequired();

        builder.Property(d=>d.DateOfBirth)
            .IsRequired(false);

        builder.Property(d=>d.JoinDate)
            .IsRequired(false);

        // Relationships Configuration 

        builder.HasOne(d => d.Department)
            .WithMany(dep => dep.Employees)
            .HasForeignKey(d => d.DepartmentId);

        // Employee → Designation (Many-to-One)

        builder.HasOne(d => d.Designation)
            .WithMany(des => des.Employees)
            .HasForeignKey(d => d.DesignationId);

        // Self-referencing Relationship for Reporting Manager
        builder.HasOne(d => d.ReportingManager)
            .WithMany(mgr => mgr.DirectReports)
            .HasForeignKey(d => d.ReportingManagerId);

        // Employee → EmployeeFiles (One-to-Many)

        builder.HasMany(e => e.EmployeeFiles)
              .WithOne(ef => ef.Employee)
              .HasForeignKey(ef => ef.EmployeeId);

        builder.HasMany(e => e.Attendances)
               .WithOne(a => a.Employee)
               .HasForeignKey(a => a.EmployeeId);
        // Employee → LeaveApplications (One-to-Many)

        builder.HasMany(e => e.LeaveApplications)
                 .WithOne(la => la.Employee)
                 .HasForeignKey(la => la.EmployeeId);


        // Employee → LeaveApprovals (One-to-Many)
        builder.HasMany(e => e.LeaveApprovals)
               .WithOne(la => la.Approver)
               .HasForeignKey(la => la.ApproverId);

        // Employee → HRGroupMembers (One-to-Many)
        builder.HasMany(e => e.HRGroupMembers)
               .WithOne(hgm => hgm.Employee)
               .HasForeignKey(hgm => hgm.EmployeeId);

        // Employee → Payrolls (One-to-Many)
        builder.HasMany(e => e.Payrolls)
               .WithOne(p => p.Employee)
               .HasForeignKey(p => p.EmployeeId);

        // Employee → ProvidentFunds (One-to-Many)
        builder.HasMany(e => e.ProvidentFunds)
               .WithOne(pf => pf.Employee)
               .HasForeignKey(pf => pf.EmployeeId);

        // Employee → SalaryStructures (One-to-Many)
        builder.HasMany(e => e.SalaryStructures)
               .WithOne(ss => ss.Employee)
               .HasForeignKey(ss => ss.EmployeeId);

        // Employee → CurrentSalaryStructure (One-to-One)
        builder.HasOne(e => e.CurrentSalaryStructure)
               .WithOne(ss => ss.Employee)
               .HasForeignKey<SalaryStructure>(ss => ss.EmployeeId);
           

        // Employee → ProvidentFund (One-to-One)
        builder.HasOne(e => e.ProvidentFund)
               .WithOne(pf => pf.Employee)
               .HasForeignKey<ProvidentFund>(pf => pf.EmployeeId);

    }
}
