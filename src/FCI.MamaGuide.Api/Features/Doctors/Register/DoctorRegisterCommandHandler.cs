using FCI.MamaGuide.Api.Domain.Base;
using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Domain.Enums;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using FCI.MamaGuide.Api.Shared.Identity;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FCI.MamaGuide.Api.Features.Doctors.Register;

public sealed record DoctorRegisterCommandHandler
    : ICommandHandler<DoctorRegisterCommand>
{
    private readonly UserManager<BaseIdentityEntity> _userManager;
    private readonly IRepositoryManager _repositoryManager;
    public DoctorRegisterCommandHandler(UserManager<BaseIdentityEntity> userManager, IRepositoryManager repositoryManager)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
    }

    public async Task<Result> Handle(DoctorRegisterCommand request, CancellationToken cancellationToken)
    {
        var findDoctor = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

        if (findDoctor is not null)
            return Result.Fail($"Doctor With {request.PhoneNumber} is already exists");

        var findHospital = await _repositoryManager.Hospitals.GetByIdAsync(request.HospitalId, cancellationToken);

        if (findHospital is null)
            return Result.Fail($"Hospital With {request.HospitalId} is not exists");

        var doctorToCreate = Doctor.Create
            (request.FirstName,
             request.LastName,
             request.City,
             request.Governorate,
             request.Gender,
             request.Specialization,
             request.PhoneNumber,
             findHospital);

        var createDoctorResult = await _userManager.CreateAsync(doctorToCreate, request.Password);

        if (!createDoctorResult.Succeeded)
        {
            return Result.Fail(createDoctorResult.GetCreationErrors());
        }

        var assignRoleResult = await _userManager.AddToRoleAsync(doctorToCreate, nameof(AppRoles.Doctor));

        if (!assignRoleResult.Succeeded)
        {
            return Result.Fail(assignRoleResult.GetCreationErrors());
        }

        return Result.Ok("Create Doctor Successfully");
    }
}