using FCI.MamaGuide.Api.Domain.Entities.Core;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.DeleteArticle;

public sealed class DeleteArticleCommandHandler
    : ICommandHandler<DeleteArticleCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public DeleteArticleCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var findArticle = await _repositoryManager.Articles.GetByIdAsync(request.Id, cancellationToken);

        if (findArticle is null)
            return Result.Fail("Article not found");

        Article.Delete(findArticle);

        await _repositoryManager.Articles.UpdateAsync(findArticle);

        if (await _repositoryManager.SaveChangesAsync() == 0)
            return Result.Fail("Failed to delete article");

        return Result.Ok("Article Deleted Successfully");
    }
}