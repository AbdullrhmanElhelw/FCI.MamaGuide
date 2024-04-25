namespace FCI.MamaGuide.Api.Features.Doctors.Update;

public sealed record UpdateDoctorDTO
    (string FirstName,
    string LastName,
    string Specialization,
    string City,
    string Governorate);