namespace FCI.MamaGuide.Api.Features.ProfilePictures.Repository;

public interface IProfilePictureRepository
{
    Task AddProfilePictureAsync(Guid doctorId, string path);
}