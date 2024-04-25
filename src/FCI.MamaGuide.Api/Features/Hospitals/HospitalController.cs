using FCI.MamaGuide.Api.Features.Hospitals.Requests.GetAllHospitals;
using FCI.MamaGuide.Api.Features.Hospitals.Requests.GetHospital;
using FCI.MamaGuide.Api.Shared.BaseController;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FCI.MamaGuide.Api.Features.Hospitals;

[Route(HospitalControllerRoute.Base)]
[ApiController]
public class HospitalController : ApiBaseController
{
    public HospitalController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet(HospitalControllerRoute.Get)]
    public async Task<IActionResult> GetHospital(Guid id)
    {
        var result = await _sender.Send(new GetHospitalQuery(id));

        return result.IsSuccess ?
            Ok(result.Value)
            : NotFound(result.Error);
    }

    [HttpGet(HospitalControllerRoute.GetAll)]
    public async Task<IActionResult> GetAllHospitals([FromQuery] GetAllHospitalQuery query)
    {
        var result = await _sender.Send(query);
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(result.Value.MetaData));
        return result.IsSuccess ?
            Ok(result)
            : NotFound(result.Error);
    }
}

public static class HospitalControllerRoute
{
    public const string Base = "api/hospitals";
    public const string Get = "{id}";
    public const string GetAll = "";
}