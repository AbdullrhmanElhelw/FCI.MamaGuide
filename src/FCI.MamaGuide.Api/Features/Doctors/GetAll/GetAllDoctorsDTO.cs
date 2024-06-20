namespace FCI.MamaGuide.Api.Features.Doctors.GetAll;

public sealed record GetAllDoctorsDTO
    (Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string City,
    string Speciality,
    string Governorate,
    bool Gender);