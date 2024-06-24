using MediatR;
using Order.Application.Models.Response;
using Order.Domain.Enums;
using Shared.Library;

namespace Order.Application.Features.Orders.Commands.RegisterOrder;

public record RegisterOrderCommand(
    int CompanyId,
    decimal Amount,
    Currency Currency)
    : IRequest<Result<OrderResponse>>;