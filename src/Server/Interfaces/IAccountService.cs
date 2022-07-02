using Google.Apis.Auth;
using Server.Models.Domain;

namespace Server.Interfaces;

public interface IAccountService
{
    Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthPayload payload);
}
