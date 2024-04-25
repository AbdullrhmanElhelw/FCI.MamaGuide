using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FCI.MamaGuide.Api.Features.Admins.GetAdmin;

public sealed class GetAdminQueryHandler
    : IQueryHandler<GetAdminQuery, GetAdminDTO>
{
    private readonly UserManager<Admin> _userManager;

    public GetAdminQueryHandler(UserManager<Admin> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<GetAdminDTO>> Handle(GetAdminQuery request, CancellationToken cancellationToken)
    {
        var findAdmin = await _userManager.Users
            .Where(a => !a.IsDeleted && a.Id == request.Id)
            .Select(a => new GetAdminDTO(a.Id, a.FirstName, a.LastName, a.PhoneNumber, a.CreatedOnUtc))
            .FirstOrDefaultAsync();

        if (findAdmin is null)
            return Result.Fail<GetAdminDTO>("Admin not found");

        return Result.Ok(findAdmin);
    }
}