using System.ComponentModel.DataAnnotations;

namespace MeetupAPI.Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [MaxLength(30)]
    public string Username { get; set; }

    [MaxLength(60)]
    public string PasswordHash { get; set; }

    [MaxLength(300)]
    public string RefreshToken { get; set; } = "";

    public DateTime TokenCreated { get; set; }

    public DateTime TokenExpires { get; set; }
}
