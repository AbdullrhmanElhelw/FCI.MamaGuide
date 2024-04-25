using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Queries;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.GetNotVerifiedArticles;

public sealed record GetNotVerifiedArticlesQuery
    (
    Guid DoctorId,
    int PageNumber,
    int PageSize) : IQuery<PagedList<GetNotVerifiedArticlesDTO>>;