using Order.Application.Models.Request;

namespace Order.Application.Interfaces;

public interface IOrderService
{
    Task ComputeOrdersAndNotifyAsync(OrderComputeRequest request);
}