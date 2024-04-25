using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.Doctors.Login;

public sealed record DoctorLoginCommand
    (string PhoneNumber, string Password) : ICommand<string>;