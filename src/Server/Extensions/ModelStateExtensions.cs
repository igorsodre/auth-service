using Microsoft.AspNetCore.Mvc.ModelBinding;
using Server.Models.ResponseDtos;

namespace Server.Extensions;

public static class ModelStateExtensions
{
    public static ErrorResponse ToErrorResponse(this ModelStateDictionary modelState)
    {
        return new ErrorResponse
        {
            ErrorMessages = modelState.Where(x => x.Value?.Errors.Count > 0)
                .SelectMany(
                    validationResult => validationResult.Value?.Errors.Select(message => message.ErrorMessage) ??
                                        Enumerable.Empty<string>()
                )
                .ToList()
        };
    }
}
