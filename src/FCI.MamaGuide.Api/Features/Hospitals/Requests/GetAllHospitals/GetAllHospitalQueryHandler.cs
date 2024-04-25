using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Queries;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;

namespace FCI.MamaGuide.Api.Features.Hospitals.Requests.GetAllHospitals;

public sealed class GetAllHospitalQueryHandler
    : IQueryHandler<GetAllHospitalQuery, PagedList<GetAllHospitalDTO>>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetAllHospitalQueryHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<PagedList<GetAllHospitalDTO>>> Handle(GetAllHospitalQuery request, CancellationToken cancellationToken)
    {
        var getHospitals = await _repositoryManager.Hospitals.GetHospitalsAsync(request.PageNumber,
                                                                           request.PageSize,
                                                                           request.Search);

        var hospitals = getHospitals.Select(x => new GetAllHospitalDTO(x.Name,
                                                                       x.Governorate,
                                                                       x.City,
                                                                       x.PhoneNumber,
                                                                       x.Street));

        var count = await _repositoryManager.Hospitals.GetHospitalCountAsync();

        var result = new PagedList<GetAllHospitalDTO>(hospitals, count, request.PageNumber, request.PageSize);

        return Result.Ok(result);
    }
}