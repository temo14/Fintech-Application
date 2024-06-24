using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Payment.Domain.Interfaces;
using Shared.Library;

namespace Payment.Infrastructure.EventBus;

public class OrderCreatedEventConsumer(
    IServiceScopeFactory serviceScopeFactory
    ) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var serviceProvider = scope.ServiceProvider;
        var transactionRepository = serviceProvider.GetRequiredService<ITransactionRepository>();

        var transaction = await transactionRepository.SaveAsync(new Domain.Entities.Transaction
        {
            orderid = context.Message.Id
        });

        if (transaction is null) return;
    }
}