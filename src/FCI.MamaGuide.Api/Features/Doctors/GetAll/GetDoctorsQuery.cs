using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Queries;

namespace FCI.MamaGuide.Api.Features.Doctors.GetAll;

public sealed record GetDoctorsQuery
    (int PageNumber,
     int PageSize) : IQuery<PagedList<GetAllDoctorsDTO>>;