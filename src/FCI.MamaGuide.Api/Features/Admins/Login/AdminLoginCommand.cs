using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.Admins.Login;

public sealed record AdminLoginCommand
    (string PhoneNumber,
    string Password) : ICommand;