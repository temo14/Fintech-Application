using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payment.Application.Features.Transaction.Command.Proccess;

namespace Payment.Api.Controllers;

[ApiController]
public class PaymentController(IMediator mediator) : ControllerBase
{
    [HttpPost("process")]
    public async Task<IActionResult> Process([FromBody] ProccessTransactionCommand request)
        => Ok(await mediator.Send(request));
}