namespace Server.Exceptions;

public abstract class BaseException : Exception
{
    public BaseException(string message) : base(message) { }

    public BaseException() { }

    public abstract IList<string> ErrorMessages();
}
