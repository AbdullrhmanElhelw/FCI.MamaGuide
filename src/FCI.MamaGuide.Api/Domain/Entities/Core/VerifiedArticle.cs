using FCI.MamaGuide.Api.Domain.Entities.Identity;

namespace FCI.MamaGuide.Api.Domain.Entities.Core;

public sealed class VerifiedArticle
{
    private VerifiedArticle(Guid articleId, Guid adminId)
    {
        ArticleId = articleId;
        AdminId = adminId;
        VerifiedAt = DateTime.UtcNow;
    }

    public Guid ArticleId { get; private set; }
    public Article Article { get; private set; }

    public Guid AdminId { get; private set; }
    public Admin Admin { get; private set; }

    public DateTime VerifiedAt { get; private set; }

    public static VerifiedArticle Create(Guid articleId, Guid adminId)
    {
        return new VerifiedArticle(articleId, adminId);
    }
}