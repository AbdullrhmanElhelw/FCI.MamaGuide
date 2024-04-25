using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Queries;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.GetNotVerifiedArticles;

public sealed class GetNotVerifiedArticlesQueryHandler
    : IQueryHandler<GetNotVerifiedArticlesQuery, PagedList<GetNotVerifiedArticlesDTO>>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetNotVerifiedArticlesQueryHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<PagedList<GetNotVerifiedArticlesDTO>>> Handle(GetNotVerifiedArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await _repositoryManager.Articles.GetAllNotVerifiedAsync(request.DoctorId, request.PageNumber, request.PageSize);

        if (articles is null)
            return Result.Fail<PagedList<GetNotVerifiedArticlesDTO>>("No articles found");

        var artilcesDto = articles.Select(x => new GetNotVerifiedArticlesDTO(x.Id,
                                                                 x.Title,
                                                                 x.Content,
                                                                 x.IsVerified,
                                                                 x.CreatedOnUtc,
                                                                 x.Doctor.FirstName + x.Doctor.LastName));

        var count = await _repositoryManager.Articles.NotVerifiedCountAsync();

        var result = new PagedList<GetNotVerifiedArticlesDTO>(artilcesDto, count, request.PageNumber, request.PageSize);

        return Result.Ok(result);
    }
}