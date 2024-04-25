using FCI.MamaGuide.Api.Domain.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FCI.MamaGuide.Api.Shared.Identity;

public static class IdentityExtension
{
    public static async Task<BaseIdentityEntity> FindByPhoneNumberAsync<TUser>(this UserManager<BaseIdentityEntity> userManager, string phoneNumber)
        where TUser : class
    {
        return await userManager?.Users?.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
    }

    public static string GetCreationErrors(this IdentityResult result)
    {
        return string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
    }
}