using MassTransit;
using Order.Application.Interfaces;
using Order.Domain.Interfaces;

namespace Order.Infrastructure.EventBus;

internal class EventBusService(
    IPublishEndpoint publishEndpoint
    ) : IEventBusService
{
    public void Publish<T>(T message)
    {
        publishEndpoint.Publish(message);
    }
}