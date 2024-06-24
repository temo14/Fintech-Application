using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Domain.Interfaces;

namespace Order.Persistance.Repositories;

internal class OrderRepository(
    OrderDbContext context
    ) : IOrderRepository
{
    public async Task<OrderEntity> AddOrderAsync(Domain.Entities.OrderEntity order)
    {
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return order;
    }

    public async Task<List<OrderEntity>> GetOrdersByCompanyAsync(int companyId, DateTime? fromDate = null)
    {
        var query = context.Orders.Where(o => o.companyid == companyId);

        if (fromDate.HasValue)
        {
            query = query.Where(o => o.createdat >= fromDate.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<OrderEntity> GetOrderByIdAsync(int orderId)
    {
        return await context.Orders.FirstOrDefaultAsync(x => x.id == orderId);
    }

    public async Task UpdateOrderByIdAsync(int orderId, Action<OrderEntity> updateAction)
    {

        var order = await context.Orders.Where(x => x.id == orderId).FirstOrDefaultAsync();

        updateAction?.Invoke(order);

        await context.SaveChangesAsync();
    }
}