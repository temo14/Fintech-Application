using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviors;
using Shared.Extensions;
using System.Reflection;
using Shared.Auth;

namespace IdentityService.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(config => {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.ConfigureSwaggerAuthentication();

        return services;
    }
}