using AutoMapper;
using MeetupAPI.Application.DTOs;
using MeetupAPI.Domain.Entities;

namespace MeetupAPI.Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserEntryDto, User>();
        CreateMap<User, OrganizerDto>();

        CreateMap<CreateMeetupDto, Meetup>();
        CreateMap<UpdateMeetupDto, Meetup>();
        CreateMap<Meetup, MeetupResponseDto>();
    }
}
