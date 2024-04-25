using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.ReviewArticle.Requests.RejectArticle;

public sealed record RejectArticleCommand
    (Guid ArticleId) : ICommand;