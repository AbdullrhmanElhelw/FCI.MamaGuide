using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Shared.ApiResponse;
using FCI.MamaGuide.Api.Shared.CQRS.Commands;
using FCI.MamaGuide.Api.Shared.Identity;
using Microsoft.AspNetCore.Identity;

namespace FCI.MamaGuide.Api.Features.Doctors.Delete;

public class DeleteDoctorCommandCommandHandler
    : ICommandHandler<DeleteDoctorCommand>
{
    private readonly UserManager<Doctor> _userManager;

    public DeleteDoctorCommandCommandHandler(UserManager<Doctor> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctorIsExists = await _userManager.FindByIdAsync(request.DoctorId.ToString());
        if (doctorIsExists?.IsDeleted is true)
        {
            return Result.Fail("Doctor is already Locked Or Deleted");
        }

        var result = await _userManager.UpdateAsync(Doctor.Delete(doctorIsExists));

        if (!result.Succeeded)
        {
            return Result.Fail(result.GetCreationErrors());
        }

        return Result.Ok("Doctor deleted successfully");
    }
}