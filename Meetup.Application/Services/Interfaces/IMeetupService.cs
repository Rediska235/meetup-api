using MeetupAPI.Application.DTOs;

namespace MeetupAPI.Application.Services.Interfaces;

public interface IMeetupService
{
    IEnumerable<MeetupResponseDto> GetMeetups();
    MeetupResponseDto GetMeetupById(int id);
    MeetupResponseDto CreateMeetup(CreateMeetupDto createMeetupDto);
    MeetupResponseDto UpdateMeetup(UpdateMeetupDto updateMeetupDto);
    void DeleteMeetup(int id);
}
