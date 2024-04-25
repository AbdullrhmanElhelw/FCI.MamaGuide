using FCI.MamaGuide.Api.Domain.Entities.Core;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.CreateArticle;

public sealed class CreateArticleCommandHandler
    : ICommandHandler<CreateArticleCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public CreateArticleCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = Article.Create(request.Title, request.Content, request.DoctorId);

        await _repositoryManager.Articles.AddAsync(article, cancellationToken);

        if (await _repositoryManager.SaveChangesAsync(cancellationToken) == 0)
        {
            return Result.Fail("Failed to create article");
        }

        return Result.Ok("Article Created Successfully");
    }
}