namespace IdentityService.Application.Interfaces;

public interface IAuthService
{
    string GenerateAccessToken(int clientId, out DateTime expiresAt);
    string GenerateApiKey();
    string GenerateApiSecret();
}