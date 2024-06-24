using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Orders.Commands.RegisterOrder;
using Order.Application.Features.Orders.Queries.ComputCompanyOrders;
using Order.Application.Models.Request;
using Shared.Auth;

namespace Order.Api.Controllers;

[ApiController]
public class CompaniesController(IMediator mediator) : ControllerBase
{
    [HttpPost("orders")]
    public async Task<IActionResult> RegisterOrder([FromBody] RegisterOrderRequest request)
        => Ok(await mediator.Send(new RegisterOrderCommand(
                        CompanyId: TokenHelper.GetCompanyIdFromContext(HttpContext),
                        request.Amount,
                        request.Currency)));

    [HttpGet("/orders/compute")]
    public async Task<IActionResult> RetrieveCompanyDetails()
    {
        var result = await mediator.Send(new ComputCompanyOrdersQuery(
            CompanyId: TokenHelper.GetCompanyIdFromContext(HttpContext),
            Token: HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "")));

        if (result.StatusCode == StatusCodes.Status202Accepted)
        {
            return Accepted(result);
        }

        return Ok(result);
    }
}