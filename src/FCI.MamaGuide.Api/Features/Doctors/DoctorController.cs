using FCI.MamaGuide.Api.Domain.Enums;
using FCI.MamaGuide.Api.Features.Doctors.Delete;
using FCI.MamaGuide.Api.Features.Doctors.GetAll;
using FCI.MamaGuide.Api.Features.Doctors.GetDoctor;
using FCI.MamaGuide.Api.Features.Doctors.Login;
using FCI.MamaGuide.Api.Features.Doctors.Register;
using FCI.MamaGuide.Api.Features.Doctors.Update;
using FCI.MamaGuide.Api.Shared.BaseController;
using FCI.MamaGuide.Api.Shared.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FCI.MamaGuide.Api.Features.Doctors;

[Route(DoctorControllerRoute.Base)]
[ApiController]
public class DoctorController : ApiBaseController
{
    private readonly UserUtility _userUtility;

    public DoctorController(ISender sender, UserUtility userUtility)
        : base(sender)
    {
        _userUtility = userUtility;
    }

    [HttpGet(DoctorControllerRoute.Get)]
    public async Task<IActionResult> GetDoctor(Guid id)
    {
        var result = await _sender.Send(new GetDoctorQuery(id));
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpGet(DoctorControllerRoute.GetAll)]
    public async Task<IActionResult> GetAllDoctors([FromQuery] GetDoctorsQuery query)
    {
        var result = await _sender.Send(query);
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(result.Value.MetaData));
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpPost(DoctorControllerRoute.Register)]
    public async Task<IActionResult> RegisterDoctor(DoctorRegisterCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpPost(DoctorControllerRoute.Login)]
    public async Task<IActionResult> LoginDoctor(DoctorLoginCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpPut(DoctorControllerRoute.Update)]
    [Authorize(Roles = nameof(AppRoles.Doctor))]
    public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorDTO updateDoctor)
    {
        var id = Guid.Parse(_userUtility.GetUserId());
        var result = await _sender.Send(new UpdateDoctorCommand(id,
                                                                updateDoctor.FirstName,
                                                                updateDoctor.LastName,
                                                                updateDoctor.Specialization,
                                                                updateDoctor.City,
                                                                updateDoctor.Governorate));
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpDelete(DoctorControllerRoute.Delete)]
    [Authorize(Roles = nameof(AppRoles.Admin))]
    public async Task<IActionResult> DeleteDoctor(Guid id)
    {
        var result = await _sender.Send(new DeleteDoctorCommand(id));
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }
}

public static class DoctorControllerRoute
{
    public const string Base = "api/Doctor";
    public const string Register = "register";
    public const string Login = "login";
    public const string Update = "update";
    public const string Delete = "delete/{id:guid}";
    public const string Get = "get/{id:guid}";
    public const string GetAll = "get-all";
}