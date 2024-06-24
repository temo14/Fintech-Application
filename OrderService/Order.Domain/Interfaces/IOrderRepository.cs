namespace Order.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Entities.OrderEntity> AddOrderAsync(Entities.OrderEntity order);
    Task<Entities.OrderEntity> GetOrderByIdAsync(int orderId);
    Task<List<Entities.OrderEntity>> GetOrdersByCompanyAsync(int companyId, DateTime? fromDate = null);
    Task UpdateOrderByIdAsync(int orderId, Action<Entities.OrderEntity> updateAction);
}