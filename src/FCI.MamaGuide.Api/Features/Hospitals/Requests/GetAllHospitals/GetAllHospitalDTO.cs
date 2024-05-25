namespace FCI.MamaGuide.Api.Features.Hospitals.Requests.GetAllHospitals;

public sealed record GetAllHospitalDTO
    (
    Guid Id,
    string Name,
    string Governorate,
    string City,
    string PhoneNumber,
    string Street
    );