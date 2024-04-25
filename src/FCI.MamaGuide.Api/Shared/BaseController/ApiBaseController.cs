using FCI.MamaGuide.Api.Shared.ApiResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCI.MamaGuide.Api.Shared.BaseController;

[Route("api/[controller]")]
[ApiController]
public class ApiBaseController : ControllerBase
{
    protected readonly ISender _sender;

    protected ApiBaseController(ISender sender)
    {
        _sender = sender;
    }

    protected IActionResult HandleFailure(Result result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
             BadRequest(
                CreateProblemDetails(
                    "Validation error", StatusCodes.Status400BadRequest,
                    result.Error!,
                    validationResult.Errors)
                ),

            _ =>
             BadRequest(
                CreateProblemDetails(
                    "Validation error", StatusCodes.Status400BadRequest,
                    result.Error!)
                ),
        };
    }

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new ProblemDetails
        {
            Title = title,
            Status = status,
            Detail = error.Message,
            Extensions =
            {
                ["errors"] = errors
            }
        };
}