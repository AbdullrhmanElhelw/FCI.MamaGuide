using FCI.MamaGuide.Api.Features.ProfilePictures.Requests.SetProfilePicture;
using FCI.MamaGuide.Api.Shared.BaseController;
using FCI.MamaGuide.Api.Shared.Utility;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCI.MamaGuide.Api.Features.ProfilePictures;

[Route(ProfilePictureControllerRoute.Base)]
[ApiController]
public class ProfilePictureController : ApiBaseController
{
    private readonly UserUtility _userUtility;

    public ProfilePictureController(ISender sender, UserUtility userUtility) : base(sender)
    {
        _userUtility = userUtility;
    }

    [HttpPost(ProfilePictureControllerRoute.Set)]
    public async Task<IActionResult> SetProfilePicture(IFormFile profilePicture)
    {
        var userId = Guid.Parse(_userUtility.GetUserId());
        if (userId == Guid.Empty)
            return Unauthorized();

        var command = new SetProfilePictureCommand(userId, profilePicture);

        var result = await _sender.Send(command);

        return result.IsSuccess ?
            Ok(result)
            : BadRequest(result);
    }
}

public static class ProfilePictureControllerRoute
{
    public const string Base = "api/profilepictures";
    public const string Get = "{id}";
    public const string Set = "";
}