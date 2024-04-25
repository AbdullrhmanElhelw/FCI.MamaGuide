using FCI.MamaGuide.Api.Shared.CQRS.Queries;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.GetArticle;

public sealed record GetArticleQuery
    (Guid Id) : IQuery<GetArticleDTO>;