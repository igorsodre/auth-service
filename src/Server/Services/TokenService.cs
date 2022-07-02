using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Server.Interfaces;

namespace Server.Services;

class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _accesskey;
    private readonly SymmetricSecurityKey _refreshKey;

    private readonly TimeSpan _accessTokenExpiry = TimeSpan.FromMinutes(7);

    private readonly TimeSpan _refreshTokenExpiry = TimeSpan.FromDays(7);

    private TokenValidationParameters? _refreshTokenValidationParams;

    private TokenValidationParameters RefreshTokenValidationParams
    {
        get
        {
            return _refreshTokenValidationParams ??= new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _refreshKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha512 },
                ClockSkew = TimeSpan.Zero
            };
        }
    }

    public TokenService(string accessTokenSecret, string refreshTokenSecret)
    {
        _accesskey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenSecret));
        _refreshKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshTokenSecret));
    }

    public TokenService(
        string accessTokenSecret,
        string refreshTokenSecret,
        TimeSpan accessTokenExpiry,
        TimeSpan refreshTokenExpiry
    ) : this(accessTokenSecret, refreshTokenSecret)
    {
        _accessTokenExpiry = accessTokenExpiry;
        _refreshTokenExpiry = refreshTokenExpiry;
    }

    public string CreateAccessToken(IEnumerable<Claim> claims)
    {
        return CreateToken(claims, _accesskey, DateTime.UtcNow.Add(_accessTokenExpiry));
    }

    public string CreateRefreshToken(IEnumerable<Claim> claims)
    {
        return CreateToken(claims, _refreshKey, DateTime.UtcNow.Add(_refreshTokenExpiry));
    }

    public IDictionary<string, string> GetRefreshTokenAttributes(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        try
        {
            var principal = handler.ValidateToken(token, RefreshTokenValidationParams, out var validToken);
            return IsJwtTokenExpired(principal)
                ? new Dictionary<string, string>()
                : principal.Claims.ToDictionary(k => k.Type, v => v.Value);
        }
        catch
        {
            return new Dictionary<string, string>();
        }
    }

    private string CreateToken(IEnumerable<Claim> claims, SecurityKey key, DateTime expiry)
    {
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptior = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiry,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptior);

        return tokenHandler.WriteToken(token);
    }

    private static bool IsJwtTokenExpired(ClaimsPrincipal claimsPrincipal)
    {
        var unixExpDate = long.Parse(claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

        var expDate = DateTimeOffset.FromUnixTimeSeconds(unixExpDate).UtcDateTime;
        return expDate <= DateTime.UtcNow;
    }
}
