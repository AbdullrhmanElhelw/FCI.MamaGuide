using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;
using Microsoft.AspNetCore.Identity;

namespace FCI.MamaGuide.Api.Features.ProfilePictures.Requests.SetProfilePicture;

public sealed class SetProfilePictureCommandHandler
    : ICommandHandler<SetProfilePictureCommand>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<Doctor> _userManager;

    public SetProfilePictureCommandHandler(IRepositoryManager repositoryManager, UserManager<Doctor> userManager)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
    }

    public async Task<Result> Handle(SetProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var checkUserIsExists = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (checkUserIsExists == null)
        {
            return Result.Fail("User not found");
        }

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profilePictures", $"{request.UserId}.");

        if (path is null)
        {
            return Result.Fail("Path is null");
        }

        await using var stream = new FileStream(path, FileMode.Create);

        await request.Picture.CopyToAsync(stream);

        await _repositoryManager.ProfilePictures.AddProfilePictureAsync(request.UserId, path);

        return await _repositoryManager.SaveChangesAsync(cancellationToken) == 0
            ? Result.Fail("Failed to save profile picture")
            : Result.Ok("Image Uploaded Successfully");
    }
}