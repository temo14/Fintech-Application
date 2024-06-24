namespace Payment.Application.Interfaces;

public interface ITransactionServiceA
{
    Task ProcessPaymentAsync(int orderId);
}