namespace Payment.Domain.Entities;

public class Transaction
{
    public int id { get; set; }
    public int orderid { get; set; }
    public string? cardnumber { get; set; }
    public DateTime? cardexpirydate { get; set; }
    public DateTime createdat { get; set; } = DateTime.UtcNow;
}