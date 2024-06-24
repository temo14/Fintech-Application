using MassTransit;
using Payment.Application.Interfaces;

namespace Payment.Infrastructure.EventBus;

internal class EventBusService(
    IPublishEndpoint publishEndpoint
    ) : IEventBusService
{
    public void Publish<T>(T message)
    {
        publishEndpoint.Publish(message);
    }
}