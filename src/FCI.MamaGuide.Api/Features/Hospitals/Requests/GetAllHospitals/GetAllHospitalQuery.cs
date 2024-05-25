using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Queries;

namespace FCI.MamaGuide.Api.Features.Hospitals.Requests.GetAllHospitals;

public sealed record GetAllHospitalQuery
    (int PageNumber,
     int PageSize,
     string Search = null!) : IQuery<PagedList<GetAllHospitalDTO>>;