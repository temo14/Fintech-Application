using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Enums;
using Order.Domain.Interfaces;
using Shared.Library;

namespace Order.Infrastructure.EventBus;

public class TransactionProccesedConsumer(
    IServiceScopeFactory serviceScopeFactory
    ) : IConsumer<TransactionProccesedEvent>
{
    public async Task Consume(ConsumeContext<TransactionProccesedEvent> context)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var serviceProvider = scope.ServiceProvider;
        var orderRepository = serviceProvider.GetRequiredService<IOrderRepository>();

        var orderId = context.Message.OrderId;

        var order = await orderRepository.GetOrderByIdAsync(orderId);
        if (order == null) return;

        var today = DateTime.UtcNow.Date;
        var companyDailyCompletedAmount = await orderRepository.GetOrdersByCompanyAsync(order.companyid, today);

        var newCompletedAmount = companyDailyCompletedAmount.Sum(o => o.amount) + order.amount;

        order.status = (newCompletedAmount > 10000) ? Status.Rejected : Status.Completed;

        await orderRepository.UpdateOrderByIdAsync(context.Message.OrderId, or =>
        {
            or.status = order.status;
        });
    }
}