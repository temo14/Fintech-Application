using MediatR;
using Microsoft.AspNetCore.Http;
using Order.Application.Interfaces;
using Order.Application.Models.Request;
using Order.Application.Models.Response;
using Order.Domain.Interfaces;
using Shared.Library;

namespace Order.Application.Features.Orders.Queries.ComputCompanyOrders;

internal class ComputCompanyOrdersQueryHandler(
    IOrderRepository repository,
    IOrderService orderService,
    ICacheService cacheService)
    : IRequestHandler<ComputCompanyOrdersQuery, Result<object>>
{
    public async Task<Result<object>> Handle(ComputCompanyOrdersQuery request, CancellationToken cancellationToken)
    {
        var companyOrders = await repository.GetOrdersByCompanyAsync(request.CompanyId);

        if (companyOrders is null)
            throw new Exception("Orders doesnot exists");

        var cacheKey = $"company-{request.CompanyId}";

        var cachedData = cacheService.GetCachedData<List<ComputeOrdersResponse>>(cacheKey);

        if (cachedData == null)
        {
            _ = Task.Run(() => orderService.ComputeOrdersAndNotifyAsync(new OrderComputeRequest(
                companyOrders,
                cacheKey,
                request.Token,
                cacheService)));

            var inProgresResponse = new ComputeInProgressResponse(
                OrderStatus: "In Progress",
                Message: "Your order is in progress and you will receive an email with all details when processing completes.");

            return Result<object>.Success(inProgresResponse, StatusCodes.Status202Accepted);
        }

        return Result<object>.Success(cachedData);
    }
}