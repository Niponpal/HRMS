using HRMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Configuration;

public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
{
    public void Configure(EntityTypeBuilder<Holiday> builder)
    {
        builder.ToTable(nameof(Holiday));
        //P.K Id
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(200);
        // Configure relationship with Shift entity
        builder.HasOne(x => x.Shift)
            .WithMany(x => x.Holidays)
            .HasForeignKey(x => x.ShiftId);
        /// Ensure department names are unique
        builder.HasIndex(d => d.StartDate)
            .IsUnique();
        builder.HasIndex(d => d.EndDate)
            .IsUnique();
    }
}
