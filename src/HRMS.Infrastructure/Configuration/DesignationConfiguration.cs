using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class DesignationConfiguration:IEntityTypeConfiguration<Designation>
{
    public void Configure(EntityTypeBuilder<Designation> builder)
    {
        builder.HasKey(builder => builder.Id);
        builder.ToTable(nameof(Designation));
        builder.Property(d=> d.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Configure relationship with Employee entity

        builder.HasMany(builder => builder.Employees)
            .WithOne(builder => builder.Designation)
            .HasForeignKey(builder => builder.DesignationId);

    }
}
