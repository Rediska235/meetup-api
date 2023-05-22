using MeetupAPI.Application.DTOs;

namespace MeetupAPI.Application.Services.Interfaces;

public interface IAuthService
{
    OrganizerDto Register(UserEntryDto request);
    string Login(UserEntryDto request, string secretKey);
    string RefreshToken(string username, string secretKey);
}
