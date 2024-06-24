using MediatR;
using Payment.Application.Interfaces;
using Payment.Domain.Interfaces;
using Shared.Library;

namespace Payment.Application.Features.Transaction.Command.Proccess;

internal class ProccessTransactionCommandHandler(
    ITransactionRepository repository,
    IEventBusService eventBusService,
    ITransactionServiceA transactionServiceA,
    ITransactionServiceB transactionServiceB
    ) : IRequestHandler<ProccessTransactionCommand, Result<int>>
{
    public async Task<Result<int>> Handle(ProccessTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await repository.GetTransactionByOrderIdAsync(request.OrderId);

        if (transaction is null)
            throw new Exception($"Transaction with orderId {request.OrderId} does not exists");

        var lastDigit = int.Parse(request.CardNumber.Last().ToString());

        _ = Task.Run(() => lastDigit % 2 == 0
            ? transactionServiceA.ProcessPaymentAsync(request.OrderId)
            : transactionServiceB.ProcessPaymentAsync(request.OrderId));

        await repository.UpdateTransactionByIdAsync(transaction.id, transaction =>
        {
            transaction.cardnumber = request.CardNumber;
            transaction.cardexpirydate = request.ExpireDate;
        });

        eventBusService.Publish(new TransactionProccesedEvent(request.OrderId));

        return Result<int>.Success(transaction.id);
    }
}