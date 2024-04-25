using FCI.MamaGuide.Api.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.MamaGuide.Api.Data.Configurations;

public sealed class BaseIdentityEntityConfiguration : IEntityTypeConfiguration<BaseIdentityEntity>
{
    public void Configure(EntityTypeBuilder<BaseIdentityEntity> builder)
    {
        builder.ToTable("Users", t => t.HasCheckConstraint("CK_Users_Gender", "[Gender] IN (0, 1)"));

        builder.UseTptMappingStrategy();

        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.City)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Governorate)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(11)
            .IsRequired();
    }
}