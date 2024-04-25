using FCI.MamaGuide.Api.Shared.CQRS.Queries;

namespace FCI.MamaGuide.Api.Features.Hospitals.Requests.GetHospital;

public sealed record GetHospitalQuery
    (Guid Id) : IQuery<GetHospitalQueryResponse>;