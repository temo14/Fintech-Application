using AutoMapper;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Models.ResponseModels;
using IdentityService.Domain.Interfaces;
using MediatR;
using Shared.Library;

namespace IdentityService.Application.Features.Companies.Queries.GetCompanyDetails;

internal class GetCompanyDetailsQueryHandler(
    IAuthService authService,
    ICompanyRepository repository,
    IMapper mapper
    ) : IRequestHandler<GetCompanyDetailsQuery, Result<CompanyResponse>>
{
    public async Task<Result<CompanyResponse>> Handle(GetCompanyDetailsQuery request, CancellationToken cancellationToken)
    {
        var company = await repository.GetCompanyByIdAsync(request.Id);

        if (company == null)
        {
            throw new Exception($"Company with ID '{request.Id}' not found.");
        }

        var companyResponseDto = mapper.Map<CompanyResponse>(company);

        return Result<CompanyResponse>.Success(companyResponseDto);
    }
}