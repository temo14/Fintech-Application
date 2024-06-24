using AutoMapper;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Models.ResponseModels;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces;
using MediatR;
using Shared.Library;

namespace IdentityService.Application.Features.Companies.Commands.RegisterCompany;

internal class RegisterCompanyCommandHandler(
    IAuthService authService,
    ICompanyRepository repository,
    IMapper mapper
    ) : IRequestHandler<RegisterCompanyCommand, Result<CompanyResponse>>
{
    public async Task<Result<CompanyResponse>> Handle(RegisterCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = mapper.Map<Company>(request);

        company.apisecret = authService.GenerateApiSecret();
        company.apikey = authService.GenerateApiKey();

        var addedCompany = await repository.AddCompanyAsync(company);
        if (addedCompany is null)
        {
            throw new Exception("Can not create company");
        }

        var resultDto = mapper.Map<CompanyResponse>(addedCompany);

        return Result<CompanyResponse>.Success(resultDto);
    }
}