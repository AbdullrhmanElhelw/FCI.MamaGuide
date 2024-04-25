namespace FCI.MamaGuide.Api.Features.Articles.Requests.GetNotVerifiedArticles;

public sealed record GetNotVerifiedArticlesDTO
    (Guid Id,
     string Title,
     string Content,
     bool IsVerified,
     DateTime CreatedOnUtc,
     string DoctorName);