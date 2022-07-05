using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Models.ResponseDtos;

namespace Server.Extensions;

public static class ExceptionExtensions
{
    public static IActionResult ToBadRequestResult(this BaseException exception)
    {
        return new BadRequestObjectResult(new { Errors = exception.ErrorMessages() });
    }

    public static ErrorResponse ToErrorResponse(this BaseException exception)
    {
        return new ErrorResponse { Errors = exception.ErrorMessages() };
    }
}
