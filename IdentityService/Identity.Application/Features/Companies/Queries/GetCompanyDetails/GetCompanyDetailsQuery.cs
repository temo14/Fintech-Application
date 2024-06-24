using IdentityService.Application.Models.ResponseModels;
using MediatR;
using Shared.Library;

namespace IdentityService.Application.Features.Companies.Queries.GetCompanyDetails;

public record GetCompanyDetailsQuery(int Id) : IRequest<Result<CompanyResponse>>;