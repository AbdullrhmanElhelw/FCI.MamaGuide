using FluentValidation;

namespace FCI.MamaGuide.Api.Features.Doctors.Update;

public class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
{
    public UpdateDoctorCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .MaximumLength(50).WithMessage("FirstName must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .MaximumLength(50).WithMessage("LastName must not exceed 50 characters.");

        RuleFor(x => x.Specialization)
            .NotEmpty().WithMessage("Specialization is required.")
            .MaximumLength(50).WithMessage("Specialization must not exceed 50 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City must not exceed 50 characters.");

        RuleFor(x => x.Governorate)
            .NotEmpty().WithMessage("Governorate is required.")
            .MaximumLength(50).WithMessage("Governorate must not exceed 50 characters.");
    }
}