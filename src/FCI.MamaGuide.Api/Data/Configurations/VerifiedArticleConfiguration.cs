using FCI.MamaGuide.Api.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.MamaGuide.Api.Data.Configurations;

public sealed class VerifiedArticleConfiguration : IEntityTypeConfiguration<VerifiedArticle>
{
    public void Configure(EntityTypeBuilder<VerifiedArticle> builder)
    {
        builder.HasKey(e => new { e.ArticleId, e.AdminId });

        builder.HasOne(e => e.Article)
            .WithMany(e => e.VerifiedArticles)
            .HasForeignKey(e => e.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Admin)
            .WithMany(e => e.VerifiedArticles)
            .HasForeignKey(e => e.AdminId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}