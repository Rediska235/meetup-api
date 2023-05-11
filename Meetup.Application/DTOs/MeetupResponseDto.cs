namespace MeetupAPI.Application.DTOs;

public class MeetupResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string? Description { get; set; }

    public OrganizerDto Organizer { get; set; }

    public string Place { get; set; }

    public DateTime Time { get; set; }
}
