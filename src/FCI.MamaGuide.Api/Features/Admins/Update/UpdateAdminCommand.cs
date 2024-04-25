using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.Admins.Update;

public sealed record UpdateAdminCommand
    (Guid AdminId,
     string FirstName,
     string LastName,
     string City,
     string Governorate) : ICommand;