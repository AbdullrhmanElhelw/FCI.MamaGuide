using FCI.MamaGuide.Api.Shared.CQRS.Commands;

namespace FCI.MamaGuide.Api.Features.Doctors.Register;

public sealed record DoctorRegisterCommand
    (string FirstName,
     string LastName,
     string City,
     string Governorate,
     bool Gender,
     string Specialization,
     string PhoneNumber,
     string Password,
     Guid HospitalId) : ICommand;