using FCI.MamaGuide.Api.Domain.Entities.Core;
using FCI.MamaGuide.Api.Shared.Repositories.BaseRepository;

namespace FCI.MamaGuide.Api.Features.Hospitals.Repository;

public interface IHospitalRepository : IGenericRepository<Hospital>
{
    Task<IEnumerable<Hospital>> GetHospitalsAsync(int pageNumber, int PageSize, string key = default);

    Task<int> GetHospitalCountAsync();
}