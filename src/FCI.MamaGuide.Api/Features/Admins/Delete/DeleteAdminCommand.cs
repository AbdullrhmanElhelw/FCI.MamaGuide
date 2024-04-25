using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.Admins.Delete;

public sealed record DeleteAdminCommand
    (Guid AdminId) : ICommand;