using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Queries;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.GetArticle;

public sealed record GetArticleQueryHandler
    : IQueryHandler<GetArticleQuery, GetArticleDTO>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetArticleQueryHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<GetArticleDTO>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
    {
        var article = await _repositoryManager.Articles.GetByIdAsync(request.Id, cancellationToken);

        if (article is null || article.IsDeleted is true)
            return Result.Fail<GetArticleDTO>("Article not found or Deleted");

        return Result.Ok(new GetArticleDTO(article.Id, article.Title, article.Content, article.IsVerified, article.CreatedOnUtc, article.DoctorId));
    }
}