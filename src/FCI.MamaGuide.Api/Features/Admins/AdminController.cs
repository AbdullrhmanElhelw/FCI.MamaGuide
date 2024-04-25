using FCI.MamaGuide.Api.Domain.Enums;
using FCI.MamaGuide.Api.Features.Admins.CreateAdmin;
using FCI.MamaGuide.Api.Features.Admins.Delete;
using FCI.MamaGuide.Api.Features.Admins.GetAdmin;
using FCI.MamaGuide.Api.Features.Admins.Login;
using FCI.MamaGuide.Api.Features.Admins.Update;
using FCI.MamaGuide.Api.Shared.BaseController;
using FCI.MamaGuide.Api.Shared.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCI.MamaGuide.Api.Features.Admins;

[Route(AdminControllerRoute.Base)]
[ApiController]
public class AdminController : ApiBaseController
{
    private readonly UserUtility _userUtility;

    public AdminController(ISender sender, UserUtility userUtility)
        : base(sender)
    {
        _userUtility = userUtility;
    }

    [HttpGet(AdminControllerRoute.Get)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = nameof(AppRoles.Admin))]
    public async Task<IActionResult> GetAdmin(Guid id)
    {
        var result = await _sender.Send(new GetAdminQuery(id));
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpPost(AdminControllerRoute.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpPost(AdminControllerRoute.Login)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] AdminLoginCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpPut(AdminControllerRoute.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = nameof(AppRoles.Admin))]
    public async Task<IActionResult> UpdateAdmin([FromBody] UpdateAdminDTO command)
    {
        var adminId = Guid.Parse(_userUtility.GetUserId());
        var result = await _sender.Send(new UpdateAdminCommand(adminId,
                                                               command.FirstName,
                                                               command.LastName,
                                                               command.City,
                                                               command.Governorate));
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpDelete(AdminControllerRoute.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = nameof(AppRoles.Admin))]
    public async Task<IActionResult> DeleteAdmin(Guid id)
    {
        var result = await _sender.Send(new DeleteAdminCommand(id));
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }
}

public static class AdminControllerRoute
{
    public const string Base = "api/Admin";
    public const string Create = "create";
    public const string Login = "login";
    public const string Update = "update";
    public const string Delete = "delete/{id:guid}";
    public const string Get = "{id:guid}";
}