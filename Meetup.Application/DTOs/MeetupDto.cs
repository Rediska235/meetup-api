using MeetupAPI.Domain.Entities;

namespace MeetupAPI.Application.DTOs;

public class MeetupDto
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public OrganizerDto Organizer { get; set; }

    public string Place { get; set; }

    public DateTime Time { get; set; }
}
