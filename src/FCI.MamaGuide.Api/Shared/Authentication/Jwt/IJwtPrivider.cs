using System.Security.Claims;

namespace FCI.MamaGuide.Api.Shared.Authentication.Jwt;

public interface IJwtProvider
{
    string CreateToken(List<Claim> claims);

    string CreateToken(string id, string phoneNumber, string Role);
}