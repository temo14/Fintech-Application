using Shared.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using FluentValidation;

namespace Shared.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException validationException)
        {
            var statusCode = HttpStatusCode.BadRequest;
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var errorMessages = validationException.Errors
                .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
                .ToArray();

            var problemDetails = Result.Failed(errorMessages);

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (Exception exception)
        {
            var statusCode = HttpStatusCode.BadRequest;
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            _logger.LogError(exception, "Exception occured: {Message}", exception.Message);

            var problemDetails = Result.Failed((int)statusCode, exception.Message, exception.StackTrace, exception.InnerException?.Message);

            await context.Response.WriteAsJsonAsync(problemDetails); ;
        }
    }
}