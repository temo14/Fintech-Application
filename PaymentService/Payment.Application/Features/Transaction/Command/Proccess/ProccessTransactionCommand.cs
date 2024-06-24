using MediatR;
using Shared.Library;

namespace Payment.Application.Features.Transaction.Command.Proccess;

public record ProccessTransactionCommand(
    int OrderId,
    string CardNumber,
    DateTime ExpireDate
    ) : IRequest<Result<int>>;
