using FluentValidation;

namespace FCI.MamaGuide.Api.Features.Hospitals.Requests.GetAllHospitals;

public sealed class GetAllHospitalQueryValidator : AbstractValidator<GetAllHospitalQuery>
{
    public GetAllHospitalQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0");
    }
}