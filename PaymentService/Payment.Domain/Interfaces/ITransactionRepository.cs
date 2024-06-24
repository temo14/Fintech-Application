using Payment.Domain.Entities;

namespace Payment.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> SaveAsync(Transaction transaction);
    Task<Transaction> GetTransactionByOrderIdAsync(int orderId);
    Task UpdateTransactionByIdAsync(int transactionId, Action<Transaction> updateAction);
}