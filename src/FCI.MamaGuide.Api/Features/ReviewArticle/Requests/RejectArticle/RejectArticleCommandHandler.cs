using FCI.MamaGuide.Api.Domain.Entities.Core;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;

namespace FCI.MamaGuide.Api.Features.ReviewArticle.Requests.RejectArticle;

public sealed class RejectArticleCommandHandler
    : ICommandHandler<RejectArticleCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public RejectArticleCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result> Handle(RejectArticleCommand request, CancellationToken cancellationToken)
    {
        var findArticle = await _repositoryManager.Articles.GetByIdAsync(request.ArticleId, cancellationToken);

        if (findArticle?.IsDeleted != false || findArticle.IsVerified)
        {
            return Result.Fail("Article not found or already verified");
        }

        if (findArticle.IsRejected)
        {
            return Result.Fail("Article already rejected");
        }

        Article.Reject(findArticle);

        await _repositoryManager.Articles.UpdateAsync(findArticle);
        if (await _repositoryManager.SaveChangesAsync() == 0)
        {
            return Result.Fail("Failed to reject article");
        }

        return Result.Ok("Article Rejected Succssfully");
    }
}