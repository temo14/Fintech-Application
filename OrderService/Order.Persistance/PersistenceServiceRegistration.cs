using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Interfaces;
using Order.Persistance.Repositories;

namespace Order.Persistance;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DbConnectionString")));

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}