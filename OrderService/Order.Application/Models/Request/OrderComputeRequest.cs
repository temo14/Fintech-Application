using Order.Application.Interfaces;
using Order.Domain.Entities;

namespace Order.Application.Models.Request;

public record OrderComputeRequest(
    IEnumerable<OrderEntity> Orders,
    string CacheKey,
    string Token,
    ICacheService CacheService);