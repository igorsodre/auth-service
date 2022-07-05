using Server.Models.ResponseDtos;

namespace Server.Extensions;

public static class ApiResponseExtensions
{
    public static ApiResponse<T> ToApiResponse<T>(this T obj)
    {
        return new ApiResponse<T>() { Data = obj };
    }
}
