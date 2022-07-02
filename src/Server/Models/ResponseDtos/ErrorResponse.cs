namespace Server.Models.ResponseDtos;

public class ErrorResponse
{
    public IList<string> ErrorMessages { get; set; } = new List<string>();
}
