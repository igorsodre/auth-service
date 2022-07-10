using Google.Apis.Auth;
using Server.Models.Domain;

namespace Server.Interfaces;

public interface IGoogleAccountService
{
    Task<GoogleJsonWebSignature.Payload> VerifyToken(ExternalAuthPayload payload);
}
