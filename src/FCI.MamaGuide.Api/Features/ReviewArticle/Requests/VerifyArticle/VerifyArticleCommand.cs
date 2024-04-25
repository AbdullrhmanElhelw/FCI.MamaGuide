using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.ReviewArticle.Requests.VerifyArticle;

public sealed record VerifyArticleCommand
    (Guid ArticleId, Guid AdminId) : ICommand;