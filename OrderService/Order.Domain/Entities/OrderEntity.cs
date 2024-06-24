using Order.Domain.Enums;

namespace Order.Domain.Entities;

public class OrderEntity
{
    public int id { get; set; }
    public int companyid { get; set; }
    public decimal amount { get; set; }
    public Currency currency{ get; set; }
    public Status status{ get; set; }
    public DateTime createdat { get; set; } = DateTime.UtcNow;
}