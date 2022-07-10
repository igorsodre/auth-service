using Server.Models.Domain;

namespace Server.Interfaces;

public interface IExternalAuthService
{
    Task<LoginResult> Login(ExternalAuthPayload payload);
}
