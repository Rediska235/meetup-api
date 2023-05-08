using System.ComponentModel.DataAnnotations;

namespace MeetupAPI.Application.DTOs;

public class OrganizerDto
{
    [MaxLength(30)]
    public string Username { get; set; } = String.Empty;
}
