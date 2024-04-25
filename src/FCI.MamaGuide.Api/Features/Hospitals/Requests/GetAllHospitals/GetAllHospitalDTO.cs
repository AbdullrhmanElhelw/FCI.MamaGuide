namespace FCI.MamaGuide.Api.Features.Hospitals.Requests.GetAllHospitals;

public sealed record GetAllHospitalDTO
    (
    string Name,
    string Governorate,
    string City,
    string PhoneNumber,
    string Street
    );