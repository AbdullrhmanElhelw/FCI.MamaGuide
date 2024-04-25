using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Domain.Enums;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.Authentication.Jwt;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, findAdminIsExists.Id.ToString()),
            new(ClaimTypes.MobilePhone, findAdminIsExists.PhoneNumber),
            new(ClaimTypes.Role,nameof(AppRoles.Admin))
        };

        var token = _jwtProvider.CreateToken(claims);

        return Result.Ok(token);
    }
}