using FCI.MamaGuide.Api.Domain.Entities.Core;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;

namespace FCI.MamaGuide.Api.Features.ReviewArticle.Requests.VerifyArticle;

public sealed class VerifyArticleCommandHandler
    : ICommandHandler<VerifyArticleCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public VerifyArticleCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result> Handle(VerifyArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _repositoryManager.Articles.GetByIdAsync(request.ArticleId, cancellationToken);

        if (article?.IsDeleted != false || article.IsRejected)
        {
            return Result.Fail("Article not found or Rejected");
        }

        if (article.IsVerified)
        {
            return Result.Fail("Article already verified");
        }

        Article.Verify(article);
        var verifiedArticle = VerifiedArticle.Create(article.Id, request.AdminId);

        await _repositoryManager.Articles.UpdateAsync(article);
        await _repositoryManager.VerifiedArticles.AddAsync(verifiedArticle, cancellationToken);

        if (await _repositoryManager.SaveChangesAsync() == 0)
        {
            return Result.Fail("Failed to verify article");
        }

        return Result.Ok("Article Verified Succssfully");
    }
}