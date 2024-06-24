using IdentityService.Application.Models.ResponseModels;
using MediatR;
using Shared.Library;

namespace IdentityService.Application.Features.Companies.Commands.RegisterCompany;

public record RegisterCompanyCommand(string Name, string Email) : IRequest<Result<CompanyResponse>>;