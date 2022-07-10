using Server.Exceptions;
using Server.Extensions;

namespace Server.Middlewares;

public class KnownExceptionsMiddleware
{
    private readonly RequestDelegate _next;

    public KnownExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(e.ToErrorResponse());
        }
    }
}
