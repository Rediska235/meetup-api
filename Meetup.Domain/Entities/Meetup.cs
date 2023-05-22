using System.ComponentModel.DataAnnotations;

namespace MeetupAPI.Domain.Entities;

public class Meetup
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public User Organizer { get; set; }

    [MaxLength(50)]
    public string Place { get; set; } = String.Empty;

    public DateTime Time { get; set; }
}
