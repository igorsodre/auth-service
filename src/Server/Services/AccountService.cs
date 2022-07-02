using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Server.config.AuthProviders;
using Server.Exceptions;
using Server.Interfaces;
using Server.Models.Domain;

namespace Server.Services;

public class AccountService : IAccountService
{
    private readonly GoogleJsonWebSignature.ValidationSettings _validationSettings;

    public AccountService(IOptions<GoogleSettings> googleSettings)
    {
        var settings = googleSettings.Value;
        _validationSettings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new[] { settings.ClientId }
        };
    }

    public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthPayload payload)
    {
        try
        {
            return await GoogleJsonWebSignature.ValidateAsync(payload.IdToken, _validationSettings);
        }
        catch
        {
            throw new TokenVerificationFailed();
        }
    }
}
