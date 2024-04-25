using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.DeleteArticle;

public sealed record DeleteArticleCommand
    (Guid Id) : ICommand;