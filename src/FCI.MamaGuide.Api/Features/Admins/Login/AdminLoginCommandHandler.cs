using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Domain.Enums;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.Authentication.Jwt;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FCI.MamaGuide.Api.Features.Admins.Login;

public sealed class AdminLoginCommandHandler
    : ICommandHandler<AdminLoginCommand>
{
    private readonly UserManager<Admin> _userManager;
    private readonly IJwtProvider _jwtProvider;

    public AdminLoginCommandHandler(UserManager<Admin> userManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
    {
        var findAdminIsExists = await _userManager.Users.FirstOrDefaultAsync
            (x => x.PhoneNumber == request.PhoneNumber);

        if (findAdminIsExists is null)
            return Result.Fail("Admin Not Found");

        var signInResult = await _userManager.CheckPasswordAsync(findAdminIsExists, request.Password);

        if (!signInResult)
            return Result.Fail("Invalid Password");

        var token = _jwtProvider.CreateToken(findAdminIsExists.Id.ToString(), findAdminIsExists.PhoneNumber, nameof(AppRoles.Admin));

        return Result.Ok(token);
    }
}