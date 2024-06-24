using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Payment.Application.Interfaces;
using Payment.Infrastructure.EventBus;
using Payment.Infrastructure.TransactionService;
using Shared.Extensions;
using Shared.Library;
using System.Text;


namespace Payment.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEventBusService, EventBusService>();
        services.AddScoped<ITransactionServiceA, TransactionServiceA>();
        services.AddScoped<ITransactionServiceB, TransactionServiceB>();
        
        services.Configure<MessageBrokerSettings>(
            configuration.GetSection("MessageBroker"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var settings = context.GetRequiredService<MessageBrokerSettings>();

                configurator.Host(new Uri(settings.Host), h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });
                configurator.ConfigureEndpoints(context);
            });
            busConfigurator.AddConsumer<OrderCreatedEventConsumer>();
        });

        services.ConfigureAuthenticationAndAuthorization();

        return services;
    }
}