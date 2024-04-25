using FluentValidation;

namespace FCI.MamaGuide.Api.Features.Articles.Requests.UpdateArticle;

public sealed class UpdateArticleValidator : AbstractValidator<UpdateArticleDTO>
{
    public UpdateArticleValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Content)
            .NotEmpty();
    }
}