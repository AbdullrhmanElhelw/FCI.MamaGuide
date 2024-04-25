using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using FCI.MamaGuide.Api.Shared.Identity;
using Microsoft.AspNetCore.Identity;

namespace FCI.MamaGuide.Api.Features.Admins.Update;

public sealed class UpdateAdminCommandHandler
    : ICommandHandler<UpdateAdminCommand>
{
    private readonly UserManager<Admin> _userManager;

    public UpdateAdminCommandHandler(UserManager<Admin> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
    {
        var adminIsExists = await _userManager.FindByIdAsync(request.AdminId.ToString());
        if (adminIsExists?.IsDeleted is true)
        {
            return Result.Fail("Admin is deleted");
        }

        var adminToUpdate = Admin.Update(adminIsExists, request.FirstName, request.LastName, request.City, request.Governorate);

        var result = await _userManager.UpdateAsync(adminToUpdate);

        if (!result.Succeeded)
        {
            return Result.Fail(result.GetCreationErrors());
        }

        return Result.Ok("Admin updated successfully");
    }
}