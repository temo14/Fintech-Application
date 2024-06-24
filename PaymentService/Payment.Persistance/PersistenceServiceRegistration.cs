using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment.Domain.Interfaces;
using Payment.Persistance.Repositories;

namespace Payment.Persistance;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DbConnectionString")));

        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}
