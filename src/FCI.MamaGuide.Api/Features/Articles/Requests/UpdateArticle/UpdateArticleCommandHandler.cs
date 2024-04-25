using FCI.MamaGuide.Api.Domain.Entities.Core;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.UpdateArticle;

public sealed class UpdateArticleCommandHandler
    : ICommandHandler<UpdateArticleCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public UpdateArticleCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var findArticle = await _repositoryManager.Articles.GetByIdAsync(request.Id, cancellationToken);

        if (findArticle is null)
        {
            return Result.Fail("Article not found");
        }

        Article.Update(findArticle, request.Title, request.Content);

        await _repositoryManager.Articles.UpdateAsync(findArticle);
        if (await _repositoryManager.SaveChangesAsync() == 0)
            return Result.Fail("Failed to update article");

        return Result.Ok("Article Update Successfully");
    }
}