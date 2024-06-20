using FCI.MamaGuide.Api.Data;
using FCI.MamaGuide.Api.Domain.Entities.Core;

namespace FCI.MamaGuide.Api.Features.ProfilePictures.Repository;

public class ProfilePictureRepository : IProfilePictureRepository
{
    private readonly MamaGuideDbContext _context;

    public ProfilePictureRepository(MamaGuideDbContext context)
    {
        _context = context;
    }

    public Task AddProfilePictureAsync(Guid doctorId, string path)
    {
        var profilePicture = ProfilePicture.Set(doctorId, path);
        _context.ProfilePictures.Add(profilePicture);
        return Task.CompletedTask;
    }
}