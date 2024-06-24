using Order.Domain.Enums;

namespace Order.Application.Models.Request;

public record RegisterOrderRequest(decimal Amount, Currency Currency);