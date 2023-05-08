using Meetup.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Domain.Entities.Meetup> Meetups { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}
