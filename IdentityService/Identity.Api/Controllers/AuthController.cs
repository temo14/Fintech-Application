using Identity.Application.Features.Companies.Commands.GenerateAccessToken;
using IdentityService.Application.Features.Companies.Commands.RegisterCompany;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("auth")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateAccessToken([FromBody] GenerateAccessTokenCommand request)
        => Ok(await mediator.Send(request));

    [HttpPost("companies")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompanyCommand request)
    => Ok(await mediator.Send(request));
}