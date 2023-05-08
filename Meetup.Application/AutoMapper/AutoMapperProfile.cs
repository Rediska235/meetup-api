using AutoMapper;
using MeetupAPI.Application.DTOs;
using MeetupAPI.Domain.Entities;

namespace MeetupAPI.Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //CreateMap<CreateUserDto, User>();
        //CreateMap<User, UserDto>();

        CreateMap<CreateMeetupDto, Meetup>();
        CreateMap<UpdateMeetupDto, Meetup>();
        //CreateMap<CreateMeetupDto, MeetupDto>();
    }
}
