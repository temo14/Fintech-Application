namespace Order.Application.Models.Response;

public record ComputeOrdersResponse(string Currency, decimal TotalAmount);