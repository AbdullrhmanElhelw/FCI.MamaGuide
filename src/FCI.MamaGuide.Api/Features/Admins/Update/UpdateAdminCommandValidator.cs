using FluentValidation;

namespace FCI.MamaGuide.Api.Features.Admins.Update;

public sealed class UpdateAdminCommandValidator : AbstractValidator<UpdateAdminCommand>
{
    public UpdateAdminCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .MaximumLength(50).WithMessage("First Name must not exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is required")
            .MaximumLength(50).WithMessage("Last Name must not exceed 50 characters");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required")
            .MaximumLength(50).WithMessage("City must not exceed 50 characters");

        RuleFor(x => x.Governorate)
            .NotEmpty().WithMessage("Governorate is required")
            .MaximumLength(50).WithMessage("Governorate must not exceed 50 characters");
    }
}