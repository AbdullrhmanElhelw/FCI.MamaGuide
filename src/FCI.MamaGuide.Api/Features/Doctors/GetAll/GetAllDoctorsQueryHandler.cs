using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FCI.MamaGuide.Api.Features.Doctors.GetAll;

public sealed class GetAllDoctorsQueryHandler
    : IQueryHandler<GetDoctorsQuery, PagedList<GetAllDoctorsDTO>>
{
    private readonly UserManager<Doctor> _userManager;

    public GetAllDoctorsQueryHandler(UserManager<Doctor> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<PagedList<GetAllDoctorsDTO>>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _userManager.Users
            .Where(d => !d.IsDeleted)
            .OrderBy(d => d.FirstName)
            .Select(d => new GetAllDoctorsDTO(d.Id,
                                              d.FirstName,
                                              d.LastName,
                                              d.PhoneNumber,
                                              d.City,
                                              d.Specialization,
                                              d.Governorate,
                                              d.Gender))
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var usersCount = await _userManager.Users.CountAsync(x => !x.IsDeleted, cancellationToken: cancellationToken);

        return Result.Ok(new PagedList<GetAllDoctorsDTO>(doctors, usersCount, request.PageNumber, request.PageSize));
    }
}