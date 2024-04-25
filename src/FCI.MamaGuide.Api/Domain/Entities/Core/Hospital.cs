using FCI.MamaGuide.Api.Domain.Base;
using FCI.MamaGuide.Api.Domain.Entities.Identity;

namespace FCI.MamaGuide.Api.Domain.Entities.Core;

public sealed class Hospital : BaseEntity
{
    private readonly List<Doctor> _doctors;

    private Hospital
        (
        string name,
        string governorate,
        string city,
        string phoneNumber,
        string street)
    {
        Name = name;
        Governorate = governorate;
        City = city;
        PhoneNumber = phoneNumber;
        Street = street;
    }

    public string Name { get; private set; }
    public string Governorate { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string PhoneNumber { get; private set; }

    public IReadOnlyCollection<Doctor> Doctors => _doctors;
}