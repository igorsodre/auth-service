using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Server.Config.AuthProviders;
using Server.Exceptions;
using Server.Interfaces;
using Server.Models.Domain;

namespace Server.Services;

public class GoogleAccountService : IGoogleAccountService
{
    private readonly GoogleJsonWebSignature.ValidationSettings _validationSettings;

    public GoogleAccountService(IOptions<GoogleSettings> googleSettings)
    {
        var settings = googleSettings.Value;
        _validationSettings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new[] { settings.ClientId }
        };
    }

    public async Task<GoogleJsonWebSignature.Payload> VerifyToken(ExternalAuthPayload payload)
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
