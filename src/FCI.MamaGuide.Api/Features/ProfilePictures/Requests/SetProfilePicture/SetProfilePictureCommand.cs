using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.ProfilePictures.Requests.SetProfilePicture;

public sealed record SetProfilePictureCommand
    (Guid UserId, IFormFile Picture) : ICommand;