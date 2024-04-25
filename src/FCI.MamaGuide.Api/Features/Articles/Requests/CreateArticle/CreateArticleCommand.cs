using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.CreateArticle;

public sealed record CreateArticleCommand
    (string Title,
    string Content,
    Guid DoctorId) : ICommand;