using MeetupAPI.Application.DTOs;
using MeetupAPI.Domain.Entities;

namespace MeetupAPI.Application.Services.Interfaces;

public interface IMeetupService
{
    IEnumerable<Meetup> GetMeetups();
    Meetup GetMeetupById(int id);
    Meetup CreateMeetup(CreateMeetupDto createMeetupDto);
    Meetup UpdateMeetup(UpdateMeetupDto updateMeetupDto);
    void DeleteMeetup(int id);
}
