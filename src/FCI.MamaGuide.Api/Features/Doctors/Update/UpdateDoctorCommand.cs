using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.Doctors.Update;

public sealed record UpdateDoctorCommand
    (Guid Id,
    string FirstName,
    string LastName,
    string Specialization,
    string City,
    string Governorate) : ICommand;