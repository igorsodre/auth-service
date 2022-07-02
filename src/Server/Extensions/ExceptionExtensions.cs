using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;

namespace Server.Extensions;

public static class ExceptionExtensions
{
    public static IActionResult ToBadRequestResult(this BaseException exception)
    {
        return new BadRequestObjectResult(new { Errors = exception.ErrorMessages() });
    }
}
