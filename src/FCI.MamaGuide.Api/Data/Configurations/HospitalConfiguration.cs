using FCI.MamaGuide.Api.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.MamaGuide.Api.Data.Configurations;

public sealed class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
{
    public void Configure(EntityTypeBuilder<Hospital> builder)
    {
        builder.ToTable("Hospitals");

        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.City)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Governorate)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Street)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(11)
            .IsRequired();

        builder.HasMany(e => e.Doctors)
            .WithOne(e => e.Hospital)
            .HasForeignKey(e => e.HospitalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}