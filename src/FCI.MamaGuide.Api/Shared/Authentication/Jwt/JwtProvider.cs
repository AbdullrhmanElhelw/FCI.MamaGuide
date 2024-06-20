using FCI.MamaGuide.Api.Shared.Authentication.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FCI.MamaGuide.Api.Shared.Authentication.Jwt;

public class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;

    public JwtProvider(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string CreateToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expire = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.ExpiryInMinutes));

        var token = new JwtSecurityToken(
            audience: _jwtSettings.Audience,
            issuer: _jwtSettings.Issuer,
            claims: claims,
            expires: expire,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    public string CreateToken(string id, string phoneNumber, string Role)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expire = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.ExpiryInMinutes));

        var claims = new List<Claim>
        {
            new Claim("Id", id),
            new Claim("PhoneNumber", phoneNumber),
            new Claim("Role", Role)
        };

        var token = new JwtSecurityToken(
            audience: _jwtSettings.Audience,
            issuer: _jwtSettings.Issuer,
            claims: claims,
            expires: expire,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}