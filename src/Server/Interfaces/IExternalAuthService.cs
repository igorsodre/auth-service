using Google.Apis.Auth;
using Server.Models.Domain;
using Server.Models.ResponseDtos;

namespace Server.Interfaces;

public interface IExternalAuthService
{
    Task<LoginResult> Login(ExternalAuthPayload payload);
}
