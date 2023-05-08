using MeetupAPI.Application.DTOs;
using MeetupAPI.Domain.Entities;

namespace MeetupAPI.Application.Services.Interfaces;

public interface IAuthService
{
    User Register(AuthUserDto request);
    string Login(AuthUserDto request, string secretKey);
    string RefreshToken(string username, string secretKey);
}
