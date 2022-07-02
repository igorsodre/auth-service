using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataAccess.Entities;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Server.config;
using Server.config.AuthProviders;
using Server.Exceptions;
using Server.Interfaces;
using Server.Models.Domain;
using Server.Models.ResponseDtos;

namespace Server.Services;

public class ExternalAuthService : IExternalAuthService
{
    private readonly IAccountService _accountService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public ExternalAuthService(
        IAccountService accountService,
        IUserRepository userRepository,
        ITokenService tokenService
    )
    {
        _accountService = accountService;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResult> Login(ExternalAuthPayload payload)
    {
        var idTokenInfo = await _accountService.VerifyGoogleToken(payload);
        var user = await _userRepository.FindByEmail(idTokenInfo.Email) ?? await CreateUser(idTokenInfo);

        var (accessToken, refreshtoken) = GenerateTokens(user);
        return new LoginResult(accessToken, refreshtoken);
    }

    private async Task<ApplicationUser> CreateUser(GoogleJsonWebSignature.Payload idTokenInfo)
    {
        await _userRepository.Create(
            new User
            {
                Email = idTokenInfo.Email,
                UserName = idTokenInfo.Email,
                Name = idTokenInfo.Name
            }
        );
        return (await _userRepository.FindByEmail(idTokenInfo.Email))!;
    }

    private (string accessToken, string refreshtoken) GenerateTokens(ApplicationUser user)
    {
        var accessToken = _tokenService.CreateAccessToken(
            new List<Claim>
            {
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(CustomTokenClaims.Id, user.Id),
            }
        );

        var refreshtoken = _tokenService.CreateRefreshToken(
            new List<Claim>
            {
                new(CustomTokenClaims.Id, user.Id),
                new(
                    CustomTokenClaims.TokenVersion,
                    user.TokenVersion.ToString()
                )
            }
        );
        return (accessToken, refreshtoken);
    }
}
