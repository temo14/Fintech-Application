using Microsoft.Extensions.Logging;
using Payment.Application.Interfaces;

namespace Payment.Infrastructure.TransactionService;

internal class TransactionServiceB(
    ILogger<TransactionServiceB> logger
    ) : ITransactionServiceB
{
    public Task ProcessPaymentAsync(int orderId)
    {
        logger.LogInformation(($"Processed in ServiceB: OrderId {orderId}"));

        return Task.CompletedTask;
    }
}