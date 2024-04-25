using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using FCI.MamaGuide.Api.Shared.Identity;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;
using Microsoft.AspNetCore.Identity;

namespace FCI.MamaGuide.Api.Features.Doctors.Update;

public sealed class UpdateDoctorCommandHandler
    : ICommandHandler<UpdateDoctorCommand>
{
    private readonly UserManager<Doctor> _doctorManager;
    private readonly IRepositoryManager _repositoryManager;

    public UpdateDoctorCommandHandler(UserManager<Doctor> doctorManager, IRepositoryManager repositoryManager)
    {
        _doctorManager = doctorManager;
        _repositoryManager = repositoryManager;
    }

    public async Task<Result> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var findDoctor = await _doctorManager.FindByIdAsync(request.Id.ToString());
        if (findDoctor is null)
            return Result.Fail("Doctor Not Found");

        var doctor = Doctor.Update(findDoctor, request.FirstName, request.LastName, request.Specialization, request.City, request.Governorate);

        var Updateresult = await _doctorManager.UpdateAsync(doctor);
        if (!Updateresult.Succeeded)
            return Result.Fail(Updateresult.GetCreationErrors());

        return Result.Ok("Doctor Updated Successfully");
    }
}