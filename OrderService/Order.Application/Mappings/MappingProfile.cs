using AutoMapper;
using Order.Application.Features.Orders.Commands.RegisterOrder;
using Order.Application.Models.Response;
using Order.Domain.Entities;

namespace Order.Application.Mappings;


internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderEntity, OrderResponse>()
            .ForMember(dest => dest.OrderId,
                opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.Currency,
                opt => opt.MapFrom(src => src.currency.ToString()))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.status.ToString()));
        
        CreateMap<RegisterOrderCommand, OrderEntity>();
    }
}