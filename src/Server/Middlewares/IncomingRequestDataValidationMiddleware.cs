using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Server.Extensions;
using Server.Models.ResponseDtos;

namespace Server.Middlewares;

public class IncomingRequestDataValidationMiddleware : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState.ToErrorResponse());
            return;
        }

        await next();
    }
}
