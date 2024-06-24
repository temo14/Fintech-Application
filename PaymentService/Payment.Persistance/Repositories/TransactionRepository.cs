using Microsoft.EntityFrameworkCore;
using Payment.Domain.Entities;
using Payment.Domain.Interfaces;

namespace Payment.Persistance.Repositories;

internal class TransactionRepository(
        AppDbContext context
        ) : ITransactionRepository
{
    public async Task<Transaction> GetTransactionByOrderIdAsync(int orderId)
    {
        return await context.Transactions.Where(x => x.orderid == orderId).FirstOrDefaultAsync();
    }

    public async Task<Transaction> SaveAsync(Transaction transaction)
    {
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        return transaction;
    }

    public async Task UpdateTransactionByIdAsync(int transactionId, Action<Transaction> updateAction)
    {
        var order = await context.Transactions.Where(x => x.id == transactionId).FirstOrDefaultAsync();

        updateAction?.Invoke(order);

        await context.SaveChangesAsync();
    }
}