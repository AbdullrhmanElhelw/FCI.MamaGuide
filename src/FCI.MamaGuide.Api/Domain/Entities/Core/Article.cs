using FCI.MamaGuide.Api.Domain.Base;
using FCI.MamaGuide.Api.Domain.Entities.Identity;

using DoctorEntity = FCI.MamaGuide.Api.Domain.Entities.Identity.Doctor;

namespace FCI.MamaGuide.Api.Domain.Entities.Core;

public sealed class Article : BaseEntity
{
    private readonly List<VerifiedArticle> _verifiedArticles;

    private Article()
    { }

    private Article
        (
        string title,
        string content,
        Guid doctorId)
    {
        Title = title;
        Content = content;
        IsVerified = false;
        IsRejected = false;
        DoctorId = doctorId;
        _verifiedArticles = [];
    }

    private Article
        (Guid id,
         string title,
         string content,
         bool isVerified,
         DateTime createdAt,
         string doctorFirstName)
    {
        Id = id;
        Title = title;
        Content = content;
        IsVerified = isVerified;
        CreatedOnUtc = createdAt;
        Doctor = DoctorEntity.Select(doctorFirstName);
    }

    public string Title { get; private set; }
    public string Content { get; private set; }
    public bool IsVerified { get; private set; }

    public bool IsRejected { get; private set; }

    public Guid DoctorId { get; private set; }
    public Doctor Doctor { get; private set; }

    public IReadOnlyCollection<VerifiedArticle> VerifiedArticles => _verifiedArticles;

    public static Article Create
        (string title,
         string content,
         Guid doctorId)
    {
        return new(title, content, doctorId);
    }

    public static Article Update(Article article, string title, string content)
    {
        article.Title = title;
        article.Content = content;
        return article;
    }

    public static Article Delete(Article article)
    {
        article.IsDeleted = true;
        return article;
    }

    public static Article Verify(Article article)
    {
        article.IsVerified = true;
        return article;
    }

    public static Article GetVerifiedArticle(Guid id, string title, string content, bool isVerified, DateTime createdAt, string doctorFirstName)
    {
        return new(id, title, content, isVerified, createdAt, doctorFirstName);
    }

    public static Article GetRejectedArticle(Guid id, string title, string content, bool isRejected, DateTime createdAt, string doctorFirstName)
    {
        return new Article
        {
            Id = id,
            Title = title,
            Content = content,
            IsRejected = isRejected,
            CreatedOnUtc = createdAt,
            Doctor = DoctorEntity.Select(doctorFirstName)
        };
    }

    public static Article Reject(Article article)
    {
        article.IsRejected = true;
        return article;
    }
}