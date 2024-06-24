namespace Payment.Application.Interfaces;

public interface ITransactionServiceB
{
    Task ProcessPaymentAsync(int orderId);
}