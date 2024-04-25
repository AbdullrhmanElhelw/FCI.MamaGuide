using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FCI.MamaGuide.Api.Features.Doctors.GetDoctor;

public sealed class GetDoctorQueryHandler
    : IQueryHandler<GetDoctorQuery, GetDoctorDTO>
{
    private readonly UserManager<Doctor> _userManager;

    public GetDoctorQueryHandler(UserManager<Doctor> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<GetDoctorDTO>> Handle(GetDoctorQuery request, CancellationToken cancellationToken)
    {
        var findDoctor = await _userManager.Users
        .Include(doctor => doctor.Hospital)
        .Include(doctor => doctor.Articles.Where(article => !article.IsDeleted && article.IsVerified)
            .OrderBy(article => article.CreatedOnUtc))
        .FirstOrDefaultAsync(doctor => doctor.Id == request.DoctorId, cancellationToken);

        if (findDoctor?.IsDeleted != false)
        {
            return Result.Fail<GetDoctorDTO>("Doctor not found");
        }

        var articles = findDoctor.Articles.Select(article => new DoctorArticleDTO(article.Id, article.Title, article.Content));

        var doctorDTO = new GetDoctorDTO(findDoctor.Id,
                                         findDoctor.FirstName,
                                         findDoctor.LastName,
                                         findDoctor.City,
                                         findDoctor.Governorate,
                                         findDoctor.PhoneNumber,
                                         findDoctor.Hospital.Name,
                                         findDoctor.Specialization,
                                         articles);

        return Result.Ok(doctorDTO);
    }
}