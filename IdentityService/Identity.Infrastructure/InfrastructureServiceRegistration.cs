using Microsoft.Extensions.DependencyInjection;
using Identity.Infrastructure.Auth;
using IdentityService.Application.Interfaces;
using Identity.Infrastructure.Encryption;
using Microsoft.Extensions.Configuration;
using Shared.Extensions;

namespace Identity.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEncryptionService, EncryptionService>();

        services.ConfigureAuthenticationAndAuthorization();

        return services;
    }
}