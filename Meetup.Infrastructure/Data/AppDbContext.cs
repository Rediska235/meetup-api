using MeetupAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Meetup> Meetups { get; set; }

    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}
