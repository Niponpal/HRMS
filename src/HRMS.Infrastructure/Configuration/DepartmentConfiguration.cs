using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(d => d.Id);
        builder.ToTable(nameof(Department));
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);
        /// Ensure department names are unique
        builder.HasIndex(d => d.Name)
            .IsUnique();
    }

    
}
