namespace FCI.MamaGuide.Api.Features.Hospitals.Requests.GetHospital;

public sealed record GetHospitalQueryResponse
    (Guid Id,
    string Name,
    string Street,
    string City,
    string Governorate);