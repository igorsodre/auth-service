using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Extensions;
using Server.Interfaces;
using Server.Models.Domain;
using Server.Models.RequestDtos;
using Server.Models.ResponseDtos;

namespace Server.Controllers;

[ApiController]
[Produces("application/json")]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IExternalAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IExternalAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("/external")]
    public async Task<ActionResult<ApiResponse<LoginResult>>> ExternalLogin(ExternalAuthRequest request)
    {
        var result = await _authService.Login(_mapper.Map<ExternalAuthPayload>(request));
        return Ok(result.ToApiResponse());
    }
}
