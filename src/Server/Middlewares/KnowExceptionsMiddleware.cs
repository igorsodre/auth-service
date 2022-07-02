using Microsoft.AspNetCore.Mvc.Filters;
using Server.Exceptions;
using Server.Extensions;

namespace Server.Middlewares;

public class KnowExceptionsMiddleware : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        try
        {
            await next();
        }
        catch (BaseException e)
        {
            context.Result = e.ToBadRequestResult();
        }
    }
}
