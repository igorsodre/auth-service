namespace Server.Exceptions;

public class TokenVerificationFailed : BaseException
{
    public TokenVerificationFailed(string message) : base(message) { }

    public TokenVerificationFailed() : base("Invalid Token ID Provided") { }

    public override IList<string> ErrorMessages()
    {
        return new List<string>() { Message };
    }
}
