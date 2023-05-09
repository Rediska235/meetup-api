using MeetupAPI.Application.DTOs;
using MeetupAPI.Domain.Entities;

namespace MeetupAPI.Application.Services.Interfaces;

public interface IMeetupService
{
    IEnumerable<MeetupDto> GetMeetups();
    MeetupDto GetMeetupById(int id);
    MeetupDto CreateMeetup(CreateMeetupDto createMeetupDto);
    MeetupDto UpdateMeetup(UpdateMeetupDto updateMeetupDto);
    void DeleteMeetup(int id);
}
