namespace Server.Models.ResponseDtos;

public class ApiResponse<T>
{
    public T Data { get; set; } = default!;
}
