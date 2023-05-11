using MeetupAPI.Application.DTOs;
using MeetupAPI.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(Summary = "Register new user")]
    public IActionResult Register(AuthUserDto request)
    {
        var user = _authService.Register(request);

        return Ok(user);
    }

    [HttpPost("login")]
    [SwaggerOperation(Summary = "Login", Description = "It takes username and password and return jwtToken. For authorization you need to insert this token in \"Authorize\".")]
    public IActionResult Login(AuthUserDto request)
    {
        string secretKey = _configuration.GetSection("JWT:Key").Value;
        var token = _authService.Login(request, secretKey);

        return Ok(token);
    }

    [HttpGet("refresh-token"), Authorize(AuthenticationSchemes = "ExpiredTokenAllowed")]
    [SwaggerOperation(Summary = "Refresh token", Description = "Gives new access and refresh token. Use this endpoint when your access token expired but you still have your valid refresh token.")]
    public IActionResult RefreshToken()
    {
        var username = User.Identity.Name;
        var secretKey = _configuration.GetSection("JWT:Key").Value;
        var refreshToken = _authService.RefreshToken(username, secretKey);

        return Ok(refreshToken);
    }
}
