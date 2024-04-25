using FCI.MamaGuide.Api.Domain.Enums;
using FCI.MamaGuide.Api.Features.ReviewArticle.Requests.RejectArticle;
using FCI.MamaGuide.Api.Features.ReviewArticle.Requests.VerifyArticle;
using FCI.MamaGuide.Api.Shared.BaseController;
using FCI.MamaGuide.Api.Shared.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCI.MamaGuide.Api.Features.ReviewArticle;

[Route(ReviewArticleControllerRoute.Base)]
[ApiController]
public class ReviewArticleController : ApiBaseController
{
    private readonly UserUtility _userUtility;

    public ReviewArticleController(ISender sender, UserUtility userUtility)
        : base(sender)
    {
        _userUtility = userUtility;
    }

    [HttpPost(ReviewArticleControllerRoute.Verify)]
    [Authorize(Roles = nameof(AppRoles.Admin))]
    public async Task<IActionResult> VerifyArticle(Guid articleId)
    {
        var adminId = Guid.Parse(_userUtility.GetUserId());
        var result = await _sender.Send(new VerifyArticleCommand(articleId, adminId));
        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPost(ReviewArticleControllerRoute.Reject)]
    [Authorize(Roles = nameof(AppRoles.Admin))]
    public async Task<IActionResult> RejectArticle(Guid articleId)
    {
        var result = await _sender.Send(new RejectArticleCommand(articleId));
        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }
}

public static class ReviewArticleControllerRoute
{
    public const string Base = "api/review-article";
    public const string Verify = "verify/{articleId}";
    public const string Reject = "reject/{articleId}";
}