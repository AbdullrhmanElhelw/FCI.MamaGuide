using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using FCI.MamaGuide.Api.Shared.Identity;
using Microsoft.AspNetCore.Identity;

namespace FCI.MamaGuide.Api.Features.Admins.Delete;

public sealed class DeleteAdminCommandHandler
    : ICommandHandler<DeleteAdminCommand>
{
    private readonly UserManager<Admin> _userManager;

    public DeleteAdminCommandHandler(UserManager<Admin> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
    {
        var adminIsExists = await _userManager.FindByIdAsync(request.AdminId.ToString());

        if (adminIsExists?.IsDeleted is true)
        {
            return Result.Fail("Admin is already deleted");
        }

        var result = await _userManager.UpdateAsync(Admin.Delete(adminIsExists));

        if (!result.Succeeded)
        {
            return Result.Fail(result.GetCreationErrors());
        }

        return Result.Ok("Admin deleted successfully");
    }
}