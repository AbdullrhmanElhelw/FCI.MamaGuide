using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Queries;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.GetRejectedArticles;

public sealed record GetRejectedArticlesQuery
    (Guid DoctorId,
    int PageNumber,
    int PageSize) : IQuery<PagedList<GetRejectedArticleDTO>>;