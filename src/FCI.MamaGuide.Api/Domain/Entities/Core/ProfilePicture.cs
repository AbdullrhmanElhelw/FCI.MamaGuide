using FCI.MamaGuide.Api.Domain.Base;
using FCI.MamaGuide.Api.Domain.Entities.Identity;

namespace FCI.MamaGuide.Api.Domain.Entities.Core;

public sealed class ProfilePicture : BaseEntity
{
    // i want to store the profile picture in the floder and save the path in the database

    private ProfilePicture()
    {
    }

    public string? Path { get; private set; }

    public Guid? DoctorId { get; private set; }
    public Doctor? Doctor { get; private set; }

    public static ProfilePicture Set(Guid doctorId, string path)
    {
        return new ProfilePicture
        {
            DoctorId = doctorId,
            Path = path
        };
    }
}