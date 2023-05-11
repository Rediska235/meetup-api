using MeetupAPI.Application.DTOs;

namespace MeetupAPI.Application.Services.Interfaces;

public interface IAuthService
{
    OrganizerDto Register(AuthUserDto request);
    string Login(AuthUserDto request, string secretKey);
    string RefreshToken(string username, string secretKey);
}
