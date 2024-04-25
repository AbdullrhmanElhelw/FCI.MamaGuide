using FluentValidation;

namespace FCI.MamaGuide.Api.Features.Admins.Login;

public class AdminLoginValidator : AbstractValidator<AdminLoginCommand>
{
    public AdminLoginValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required")
            .Matches(@"^01[0|1|2|5]\d{8}$").WithMessage("Phone number must be 11 digits");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
}