using MeetupAPI.Application.DTOs;
using MeetupAPI.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.Presentation.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public IActionResult Register(AuthUserDto request)
    {
        var user = _authService.Register(request);

        return Ok(user);
    }

    [HttpPost("login")]
    public IActionResult Login(AuthUserDto request)
    {
        string secretKey = _configuration.GetSection("JWT:Key").Value;
        var token = _authService.Login(request, secretKey);

        return Ok(token);
    }

    [HttpGet("refresh-token"), Authorize(AuthenticationSchemes = "ExpiredTokenAllowed")]
    public IActionResult RefreshToken()
    {
        var username = User.Identity.Name;
        var secretKey = _configuration.GetSection("JWT:Key").Value;
        var refreshToken = _authService.RefreshToken(username, secretKey);

        return Ok(refreshToken);
    }
}
