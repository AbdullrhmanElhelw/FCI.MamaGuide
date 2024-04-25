using FluentValidation;

namespace FCI.MamaGuide.Api.Features.Doctors.Register;

public sealed class DoctorRegisterCommandValidator : AbstractValidator<DoctorRegisterCommand>
{
    public DoctorRegisterCommandValidator()
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

        RuleFor(x => x.Specialization)
            .NotEmpty().WithMessage("Specialization is required")
            .MaximumLength(50).WithMessage("Specialization must not exceed 50 characters");

        RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("Phone Number is required")
           .Matches(@"^01[0|1|2|5]\d{8}$").WithMessage("Phone number must be 11 digits");

        RuleFor(x => x.HospitalId)
            .NotEmpty().WithMessage("Hospital Id is required");

        RuleFor(x => x.Password)
          .NotEmpty().WithMessage("Password is required")
          .MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
}