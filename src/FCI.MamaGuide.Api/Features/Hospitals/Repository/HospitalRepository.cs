using FCI.MamaGuide.Api.Data;
using FCI.MamaGuide.Api.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Vertical_Slice_Architecture.Shared.Repositories.MainRepository;

namespace FCI.MamaGuide.Api.Features.Hospitals.Repository;

public class HospitalRepository : GenericRepository<Hospital>, IHospitalRepository
{
    private readonly MamaGuideDbContext _context;

    public HospitalRepository(MamaGuideDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<int> GetHospitalCountAsync()
        => _context.Hospitals.CountAsync();

    public Task<IEnumerable<Hospital>> GetHospitalsAsync(int pageNumber, int PageSize, string key = null)
    {
        var hospitals = _context.Hospitals.AsQueryable();
        if (!string.IsNullOrEmpty(key))
        {
            hospitals = hospitals.Where(x => x.Name.Contains(key));
        }

        return Task.FromResult(hospitals.Skip((pageNumber - 1) * PageSize)
                                        .Take(PageSize)
                                        .AsEnumerable());
    }
}