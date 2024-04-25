using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.Doctors.Delete;

public sealed record DeleteDoctorCommand
    (Guid DoctorId) : ICommand;