using FCI.MamaGuide.Api.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.MamaGuide.Api.Data.Configurations;

public sealed class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");

        builder.Property(e => e.Specialization)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(e => e.Articles)
            .WithOne(e => e.Doctor)
            .HasForeignKey(e => e.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.ProfilePicture)
            .WithOne(e => e.Doctor)
            .HasForeignKey<Doctor>(e => e.ProfilePictureId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}