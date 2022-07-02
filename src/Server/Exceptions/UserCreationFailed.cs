namespace Server.Exceptions;

public class UserCreationFailed : BaseException
{
    private readonly IList<string> _errors;

    public UserCreationFailed(IList<string> errors)
    {
        _errors = errors;
    }

    public override IList<string> ErrorMessages()
    {
        return _errors;
    }
}
