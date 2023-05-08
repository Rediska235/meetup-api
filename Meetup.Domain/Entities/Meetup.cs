using System.ComponentModel.DataAnnotations;

namespace Meetup.Domain.Entities;

public class Meetup
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public User Organizer { get; set; }

    [MaxLength(50)]
    public string Place { get; set; } = String.Empty;

    public DateTime Time { get; set; }
}
