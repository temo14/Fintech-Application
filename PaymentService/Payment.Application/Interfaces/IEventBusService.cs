namespace Payment.Application.Interfaces;

public interface IEventBusService
{
    void Publish<T>(T message);
}