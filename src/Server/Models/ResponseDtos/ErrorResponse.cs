namespace Server.Models.ResponseDtos;

public class ErrorResponse
{
    public IList<string> Errors { get; set; } = new List<string>();
}
