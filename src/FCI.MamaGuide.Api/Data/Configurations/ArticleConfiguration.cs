using FCI.MamaGuide.Api.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.MamaGuide.Api.Data.Configurations;

public sealed class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Articles");

        builder.Property(e => e.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Content)
            .IsRequired();
    }
}