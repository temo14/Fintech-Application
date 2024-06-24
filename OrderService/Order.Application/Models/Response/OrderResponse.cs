namespace Order.Application.Models.Response;

public class OrderResponse
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public DateTime Createdat { get; set; }
}