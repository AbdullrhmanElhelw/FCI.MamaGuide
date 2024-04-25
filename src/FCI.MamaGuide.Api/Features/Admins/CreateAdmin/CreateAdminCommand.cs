using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.Admins.CreateAdmin;

public sealed record CreateAdminCommand
    (
    string FirstName,
    string LastName,
    string City,
    string Governorate,
    bool Gender,
    string PhoneNumber,
    string Password) : ICommand;