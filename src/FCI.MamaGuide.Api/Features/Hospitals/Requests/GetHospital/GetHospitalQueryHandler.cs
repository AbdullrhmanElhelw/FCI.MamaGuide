using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Queries;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;

namespace FCI.MamaGuide.Api.Features.Hospitals.Requests.GetHospital;

public sealed class GetHospitalQueryHandler
    : IQueryHandler<GetHospitalQuery, GetHospitalQueryResponse>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetHospitalQueryHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<GetHospitalQueryResponse>> Handle(GetHospitalQuery request, CancellationToken cancellationToken)
    {
        var checkHospitalIsExist = await _repositoryManager.Hospitals.GetByIdAsync(request.Id, cancellationToken);

        if (checkHospitalIsExist is null)
            return Result.Fail<GetHospitalQueryResponse>("Hospital not found");

        var hospital = new GetHospitalQueryResponse(checkHospitalIsExist.Id,
                                                    checkHospitalIsExist.Name,
                                                    checkHospitalIsExist.Street,
                                                    checkHospitalIsExist.City,
                                                    checkHospitalIsExist.Governorate);

        return Result.Ok(hospital);
    }
}