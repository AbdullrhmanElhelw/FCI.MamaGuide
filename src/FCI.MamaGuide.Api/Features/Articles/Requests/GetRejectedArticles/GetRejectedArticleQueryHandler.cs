using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Queries;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.GetRejectedArticles;

public sealed class GetRejectedArticleQueryHandler
    : IQueryHandler<GetRejectedArticlesQuery, PagedList<GetRejectedArticleDTO>>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetRejectedArticleQueryHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<PagedList<GetRejectedArticleDTO>>> Handle(GetRejectedArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await _repositoryManager.Articles.GetRejectedAsync(request.DoctorId, request.PageNumber, request.PageSize);

        var count = await _repositoryManager.Articles.RejectedCountAsync();

        var articlesDto = articles.Select(x => new GetRejectedArticleDTO(x.Id,
                                                                         x.Title,
                                                                         x.Content,
                                                                         x.IsRejected,
                                                                         x.CreatedOnUtc,
                                                                         x.Doctor.FirstName + x.Doctor.LastName));

        return Result.Ok(new PagedList<GetRejectedArticleDTO>(articlesDto, count, request.PageNumber, request.PageSize));
    }
}