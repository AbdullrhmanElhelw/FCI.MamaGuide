using FCI.MamaGuide.Api.Shared.CQRS.Queries;

namespace FCI.MamaGuide.Api.Features.Doctors.GetDoctor;

public sealed record GetDoctorQuery
    (Guid DoctorId) : IQuery<GetDoctorDTO>;