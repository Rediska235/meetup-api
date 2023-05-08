using System.ComponentModel.DataAnnotations;

namespace MeetupAPI.Application.DTOs;

public class CreateMeetupDto
{
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string Place { get; set; } = String.Empty;

    public DateTime Time { get; set; }
}
