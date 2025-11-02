using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration
{
    public class ShiftConfiguration:IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.HasKey(builder => builder.Id);
            builder.ToTable(nameof(Shift));

            // Properties Configuration

            builder.Property(builder => builder.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(builder => builder.StartTime)
                .IsRequired();
            builder.Property(builder => builder.EndTime)
                .IsRequired();
            builder.Property(builder => builder.Remarks)
                .HasMaxLength(500);


            // Relationships Configuration

            builder.HasMany(d => d.HolidayAssignments)
                .WithOne(h => h.Shift)
                .HasForeignKey(h => h.ShiftId);

            builder.HasMany(d => d.Holidays)
                .WithOne(h => h.Shift)
                .HasForeignKey(h => h.ShiftId);
        }
    }
}
