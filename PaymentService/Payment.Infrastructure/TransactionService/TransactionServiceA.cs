using Microsoft.Extensions.Logging;
using Payment.Application.Interfaces;

namespace Payment.Infrastructure.TransactionService;

internal class TransactionServiceA(
    ILogger<TransactionServiceA> logger) : ITransactionServiceA
{
    public Task ProcessPaymentAsync(int orderId)
    {
        logger.LogInformation($"Processed in ServiceA: OrderId {orderId}");

        return Task.CompletedTask;
    }
}