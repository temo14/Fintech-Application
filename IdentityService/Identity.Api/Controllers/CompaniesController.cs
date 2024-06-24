using IdentityService.Application.Features.Companies.Commands.RegisterCompany;
using IdentityService.Application.Features.Companies.Queries.GetCompanyDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth;

namespace IdentityService.Api.Controllers;

[ApiController]
public class CompaniesController(IMediator mediator) : ControllerBase
{
    [HttpGet("company")]
    public async Task<IActionResult> RetrieveCompanyDetails()
        => Ok(await mediator.Send(new GetCompanyDetailsQuery(TokenHelper.GetCompanyIdFromContext(HttpContext))));
}