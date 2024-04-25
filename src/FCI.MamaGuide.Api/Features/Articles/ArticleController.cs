using FCI.MamaGuide.Api.Domain.Enums;
using FCI.MamaGuide.Api.Features.Articles.Requests.CreateArticle;
using FCI.MamaGuide.Api.Features.Articles.Requests.DeleteArticle;
using FCI.MamaGuide.Api.Features.Articles.Requests.GetArticle;
using FCI.MamaGuide.Api.Features.Articles.Requests.GetNotVerifiedArticles;
using FCI.MamaGuide.Api.Features.Articles.Requests.GetRejectedArticles;
using FCI.MamaGuide.Api.Features.Articles.Requests.UpdateArticle;
using FCI.MamaGuide.Api.Shared.BaseController;
using FCI.MamaGuide.Api.Shared.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FCI.MamaGuide.Api.Features.Articles;

[Route(ArticleControllerRoute.Base)]
[ApiController]
public class ArticleController : ApiBaseController
{
    private readonly UserUtility _userUtility;

    public ArticleController(ISender sender, UserUtility userUtility)
        : base(sender)
    {
        _userUtility = userUtility;
    }

    [HttpGet(ArticleControllerRoute.Get)]
    public async Task<IActionResult> GetArticle(Guid id)
    {
        var result = await _sender.Send(new GetArticleQuery(id));
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpGet(ArticleControllerRoute.GetNotVerifiedArticles)]
    [Authorize(Roles = nameof(AppRoles.Doctor))]
    public async Task<IActionResult> GetNotVerifiedArticles([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var doctorId = Guid.Parse(_userUtility.GetUserId());
        var result = await _sender.Send(new GetNotVerifiedArticlesQuery(doctorId, pageNumber, pageSize));
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(result.Value.MetaData));

        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpGet(ArticleControllerRoute.GetRejectedArticles)]
    [Authorize(Roles = nameof(AppRoles.Doctor))]
    public async Task<IActionResult> GetRejectedArticles([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var doctorId = Guid.Parse(_userUtility.GetUserId());
        var result = await _sender.Send(new GetRejectedArticlesQuery(doctorId, pageNumber, pageSize));
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(result.Value.MetaData));
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpPost(ArticleControllerRoute.Create)]
    [Authorize(Roles = nameof(AppRoles.Doctor))]
    public async Task<IActionResult> CreateArticle(CreateArticleDTO articleDTO)
    {
        var doctorId = Guid.Parse(_userUtility.GetUserId());
        var result = await _sender.Send(new CreateArticleCommand(articleDTO.Title, articleDTO.Content, doctorId));
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpPut(ArticleControllerRoute.Update)]
    [Authorize(Roles = nameof(AppRoles.Doctor))]
    public async Task<IActionResult> UpdateArticle(Guid id, UpdateArticleDTO articleDTO)
    {
        var doctorId = Guid.Parse(_userUtility.GetUserId());
        var result = await _sender.Send(new UpdateArticleCommand(id, articleDTO.Title, articleDTO.Content));
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }

    [HttpDelete(ArticleControllerRoute.Delete)]
    [Authorize(Roles = nameof(AppRoles.Doctor))]
    public async Task<IActionResult> DeleteArticle(Guid id)
    {
        var result = await _sender.Send(new DeleteArticleCommand(id));
        return result.IsSuccess ?
            Ok(result) :
            HandleFailure(result);
    }
}

public static class ArticleControllerRoute
{
    public const string Base = "api/Article";
    public const string Create = "create-article";
    public const string Update = "update-article/{id:guid}";
    public const string Delete = "delete-article/{id:guid}";
    public const string Get = "get-article/{id:guid}";
    public const string GetNotVerifiedArticles = "get-not-verified-articles";
    public const string GetRejectedArticles = "get-rejected-articles";
}