using FCI.MamaGuide.Api.Domain.Base;
using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Domain.Enums;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using FCI.MamaGuide.Api.Shared.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FCI.MamaGuide.Api.Features.Admins.CreateAdmin;

public sealed class CreateAdminCommandHandler
    : ICommandHandler<CreateAdminCommand>
{
    private readonly UserManager<BaseIdentityEntity> _userManager;

    public CreateAdminCommandHandler(UserManager<BaseIdentityEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var checkAdminExist = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

        if (checkAdminExist is not null)
            return Result.Fail($"User With {request.PhoneNumber} is already exists");

        var adminToCreate = Admin.Create
       (
           request.FirstName,
           request.LastName,
           request.City,
           request.Governorate,
           request.Gender,
           request.PhoneNumber);

        var createAdminResult = await _userManager.CreateAsync(adminToCreate, request.Password);

        if (!createAdminResult.Succeeded)
        {
            return Result.Fail(createAdminResult.GetCreationErrors());
        }

        var assignRoleResult = await _userManager.AddToRoleAsync(adminToCreate, nameof(AppRoles.Admin));

        if (!assignRoleResult.Succeeded)
        {
            return Result.Fail(assignRoleResult.GetCreationErrors());
        }

        return Result.Ok("Create Admin Successfully");
    }
}