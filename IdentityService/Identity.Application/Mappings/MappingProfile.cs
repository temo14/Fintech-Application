using AutoMapper;
using IdentityService.Application.Features.Companies.Commands.RegisterCompany;
using IdentityService.Application.Features.Companies.Queries.GetCompanyDetails;
using IdentityService.Application.Models.ResponseModels;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Mappings;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, GetCompanyDetailsQuery>();
        CreateMap<Company, CompanyResponse>()
            .ForMember(dest => dest.CompanyId,
            opt => opt.MapFrom(src => src.id));
        CreateMap<GetCompanyDetailsQuery, Company>();
        CreateMap<RegisterCompanyCommand, Company>();
    }
}