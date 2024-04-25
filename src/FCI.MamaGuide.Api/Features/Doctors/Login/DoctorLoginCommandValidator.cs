using FluentValidation;

namespace FCI.MamaGuide.Api.Features.Doctors.Login;

public sealed class DoctorLoginCommandValidator : AbstractValidator<DoctorLoginCommand>
{
    public DoctorLoginCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("PhoneNumber is required")
            .Matches(@"^01[0-2]{1}[0-9]{8}$").WithMessage("PhoneNumber is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
}