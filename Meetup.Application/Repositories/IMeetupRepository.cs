using MeetupAPI.Domain.Entities;

namespace MeetupAPI.Application.Repositories;

public interface IMeetupRepository
{
    IEnumerable<Meetup> GetMeetups();
    Meetup GetMeetupById(int id);
    Meetup GetMeetupByName(string name);
    void InsertMeetup(Meetup meetup);
    void UpdateMeetup(Meetup meetup);
    void DeleteMeetup(int id);
    void Save();
}
