using FCI.MamaGuide.Api.Features.Articles.Requests.CreateArticle;
using FluentValidation;

namespace FCI.MamaGuide.Api.Features.Articles.Requests;

public sealed class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Content)
            .NotEmpty();

        RuleFor(x => x.DoctorId)
            .NotEmpty();
    }
}