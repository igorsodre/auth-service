using System.Security.Claims;

namespace Server.Interfaces;

public interface ITokenService
{
    public string CreateAccessToken(IEnumerable<Claim> claims);

    public string CreateRefreshToken(IEnumerable<Claim> claims);

    public IDictionary<string, string> GetRefreshTokenAttributes(string token);
}