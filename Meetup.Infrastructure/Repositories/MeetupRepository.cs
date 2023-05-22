using MeetupAPI.Application.Repositories;
using MeetupAPI.Domain.Entities;
using MeetupAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Infrastructure.Repositories;

public class MeetupRepository : IMeetupRepository
{
    private readonly AppDbContext _db;

    public MeetupRepository(AppDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Meetup> GetMeetups()
    {
        return _db.Meetups.Include(m => m.Organizer);
    }

    public Meetup GetMeetupById(int id)
    {
        return _db.Meetups.Include(m => m.Organizer).FirstOrDefault(m => m.Id == id);
    }

    public Meetup GetMeetupByName(string name)
    {
        return _db.Meetups.FirstOrDefault(m => m.Name == name);
    }

    public void InsertMeetup(Meetup meetup)
    {
        _db.Add(meetup);
    }

    public void UpdateMeetup(Meetup meetup)
    {
        _db.Update(meetup);
    }

    public void DeleteMeetup(int id)
    {
        var meetup = GetMeetupById(id);
        _db.Remove(meetup);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
