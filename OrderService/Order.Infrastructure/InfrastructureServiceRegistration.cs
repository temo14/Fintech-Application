using Order.Application.Interfaces;
using MassTransit;
using Order.Infrastructure.EventBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Extensions;
using Order.Infrastructure.Cache;
using Order.Infrastructure.OrderService.Models;

namespace Order.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrderService, OrderService.OrderService>();
        services.AddScoped<IEventBusService, EventBusService>();
        services.AddScoped<ICacheService, CacheService>();

        services.Configure<EmailConfig>(
            configuration.GetSection("EmailConfig"));
        
        services.Configure<ServiceUrls>(
            configuration.GetSection("ServiceUrls"));

        services.Configure<MessageBrokerSettings>(
            configuration.GetSection("MessageBroker"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        services.AddStackExchangeRedisCache(options =>
            options.Configuration = configuration.GetConnectionString("Cache"));

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
            busConfigurator.AddConsumer<TransactionProccesedConsumer>();
        });

        services.ConfigureAuthenticationAndAuthorization();

        return services;
    }
}