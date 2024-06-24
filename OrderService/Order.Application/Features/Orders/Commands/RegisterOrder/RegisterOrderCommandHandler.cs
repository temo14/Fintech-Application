using AutoMapper;
using MediatR;
using Order.Application.Interfaces;
using Order.Application.Models.Response;
using Order.Domain.Entities;
using Order.Domain.Interfaces;
using Shared.Library;

namespace Order.Application.Features.Orders.Commands.RegisterOrder;

internal class RegisterOrderCommandHandler(
    IOrderRepository repository,
    IEventBusService eventBusService,
    ICacheService cacheService,
    IMapper mapper
    ) : IRequestHandler<RegisterOrderCommand, Result<OrderResponse>>
{
    public async Task<Result<OrderResponse>> Handle(RegisterOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = mapper.Map<OrderEntity>(request);

        var addedOrder = await repository.AddOrderAsync(orderEntity);

        if (addedOrder is null)
        {
            throw new Exception("Order can not added.");
        }

        var orderResponse = mapper.Map<OrderResponse>(addedOrder);

        eventBusService.Publish(new OrderCreatedEvent(addedOrder.id));

        cacheService.DeleteCachedData($"company-{addedOrder.companyid}");

        return Result<OrderResponse>.Success(orderResponse);
    }
}