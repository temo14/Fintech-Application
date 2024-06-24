using MediatR;
using Order.Application.Models.Response;
using Shared.Library;

namespace Order.Application.Features.Orders.Queries.ComputCompanyOrders;

public record ComputCompanyOrdersQuery(int CompanyId, string Token) : IRequest<Result<object>>;