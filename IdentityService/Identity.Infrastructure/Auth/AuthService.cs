using IdentityService.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Shared.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Infrastructure.Auth;

internal class AuthService : IAuthService
{

    public string GenerateApiKey() => Guid.NewGuid().ToString();

    public string GenerateApiSecret() => Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));

    public string GenerateAccessToken(int companyId, out DateTime expiresAt)
    {
        expiresAt = DateTime.UtcNow.AddMinutes(JwtSettings.AccessTokenExpirationInMinutes);

        var secret = Encoding.UTF8.GetBytes(JwtSettings.Key);
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, companyId.ToString()),
        };

        var token = new JwtSecurityToken(
           claims: claims,
           issuer: JwtSettings.Issuer,
           expires: expiresAt,
           signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    public string? GetClientIdFromAccessToken(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        if (tokenHandler.ReadToken(accessToken) is JwtSecurityToken securityToken)
        {
            var clientIdClaim = securityToken.Claims.FirstOrDefault(claim =>
                claim.Type == ClaimTypes.NameIdentifier);

            if (clientIdClaim != null)
            {
                return clientIdClaim.Value;
            }
        }

        return null;
    }
}