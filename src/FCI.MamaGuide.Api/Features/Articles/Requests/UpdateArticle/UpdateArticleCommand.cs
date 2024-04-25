using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.UpdateArticle;

public sealed record UpdateArticleCommand
    (Guid Id,
    string Title,
    string Content) : ICommand;