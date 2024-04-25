namespace FCI.MamaGuide.Api.Features.Articles.Requests.GetRejectedArticles;

public sealed record GetRejectedArticleDTO
    (Guid Id,
    string Title,
    string Content,
    bool IsRejected,
    DateTime CreatedOnUtc,
    string DoctorName);