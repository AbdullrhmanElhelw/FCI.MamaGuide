namespace FCI.MamaGuide.Api.Features.Articles.Requests.GetArticle;

public sealed record GetArticleDTO
    (Guid Id,
     string Title,
     string Content,
     bool IsVerified,
     DateTime CreatedOnUtc,
     Guid DoctorId);