using System.ComponentModel.DataAnnotations;

namespace MeetupAPI.Application.DTOs;

public class AuthUserDto
{
    [MaxLength(30)]
    public string Username { get; set; }

    [MaxLength(60)]
    public string Password { get; set; }
}
