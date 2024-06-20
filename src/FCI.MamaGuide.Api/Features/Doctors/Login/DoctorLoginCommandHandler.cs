﻿using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Domain.Enums;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.Authentication.Jwt;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FCI.MamaGuide.Api.Features.Doctors.Login;

public sealed class DoctorLoginCommandHandler
    : ICommandHandler<DoctorLoginCommand, string>
{
    private readonly UserManager<Doctor> _userManager;
    private readonly IJwtProvider _jwtProvider;

    public DoctorLoginCommandHandler(UserManager<Doctor> userManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(DoctorLoginCommand request, CancellationToken cancellationToken)
    {
        var findDoctor = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber, cancellationToken);

        if (findDoctor is null)
            return Result.Fail<string>("Invalid phone number or password");

        var isPasswordValid = await _userManager.CheckPasswordAsync(findDoctor, request.Password);

        if (!isPasswordValid)
            return Result.Fail<string>("Invalid phone number or password");

        var token = _jwtProvider.CreateToken(findDoctor.Id.ToString(), findDoctor.PhoneNumber, nameof(AppRoles.Doctor));

        return Result.Ok(token);
    }
}