using Microsoft.AspNetCore.Http;
using Shared.Library;

namespace Shared.Middleware;

public class CustomUnauthorizedMiddleware(
    RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized &&
            !context.Response.HasStarted)
        {
            var methodResult = Result.Failed(StatusCodes.Status401Unauthorized, "Unauthorized");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(methodResult);
        }
    }
}