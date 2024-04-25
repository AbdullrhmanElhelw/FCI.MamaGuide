using FCI.MamaGuide.Api.Shared.CQRS.Queries;

namespace FCI.MamaGuide.Api.Features.Admins.GetAdmin;

public sealed record GetAdminQuery
    (Guid Id) : IQuery<GetAdminDTO>;