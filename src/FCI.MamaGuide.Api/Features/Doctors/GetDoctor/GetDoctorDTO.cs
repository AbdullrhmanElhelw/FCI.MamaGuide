namespace FCI.MamaGuide.Api.Features.Doctors.GetDoctor;

public sealed record GetDoctorDTO
    (Guid DoctorId,
     string FirstName,
     string LastName,
     string City,
     string Governorate,
     string PhoneNumber,
     string HospitalName,
     string Specialization,
     IEnumerable<DoctorArticleDTO> Articles);

public sealed record DoctorArticleDTO
    (Guid ArticleId,
     string Title,
     string Content);