using AutoMapper;
using MeetupAPI.Application.DTOs;
using MeetupAPI.Application.Repositories;
using MeetupAPI.Application.Services.Interfaces;
using MeetupAPI.Domain.Entities;
using MeetupAPI.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace MeetupAPI.Application.Services.Implementations;

public class MeetupService : IMeetupService
{
    private readonly IMeetupRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly HttpContext _httpContext;

    public MeetupService(
        IMeetupRepository repository, 
        IUserRepository userRepository,
        IMapper mapper, 
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _userRepository = userRepository;
        _mapper = mapper;
        _httpContext = httpContextAccessor.HttpContext;
    }

    public IEnumerable<MeetupDto> GetMeetups()
    {
        var meetups = _repository.GetMeetups();
        return _mapper.Map<IEnumerable<MeetupDto>>(meetups);
    }

    public MeetupDto GetMeetupById(int id)
    {
        var meetup = _repository.GetMeetupById(id);
        if (meetup == null)
        {
            throw Exceptions.MeetupNotFound;
        }

        return _mapper.Map<MeetupDto>(meetup);
    }

    public MeetupDto CreateMeetup(CreateMeetupDto createMeetupDto)
    {
        var meetupName = createMeetupDto.Name.Trim();
        if (AlreadyExists(meetupName))
        {
            throw Exceptions.MeetupAlreadyExists;
        }

        var meetup = _mapper.Map<Meetup>(createMeetupDto);

        var username = _httpContext.User.Identity.Name;
        var user = _userRepository.GetUserByUsername(username);
        meetup.Organizer = user;

        _repository.InsertMeetup(meetup);
        _repository.Save();

        return _mapper.Map<MeetupDto>(meetup);
    }

    public MeetupDto UpdateMeetup(UpdateMeetupDto updateMeetupDto)
    {
        var meetup = _repository.GetMeetupById(updateMeetupDto.Id);
        if(meetup == null)
        {
            throw Exceptions.MeetupNotFound;
        }

        var username = _httpContext.User.Identity.Name;
        if (meetup.Organizer.Username != username)
        {
            throw Exceptions.NotYourMeetup;
        }

        var meetupName = updateMeetupDto.Name.Trim();
        if (meetupName != meetup.Name && AlreadyExists(meetupName))
        {
            throw Exceptions.MeetupAlreadyExists;
        }

        var user = _userRepository.GetUserByUsername(username);
        meetup.Organizer = user;
        meetup.Name = meetupName;
        meetup.Description = updateMeetupDto.Description;
        meetup.Time = updateMeetupDto.Time;
        meetup.Place = updateMeetupDto.Place;

        _repository.UpdateMeetup(meetup);
        _repository.Save();

        return _mapper.Map<MeetupDto>(meetup);
    }

    public void DeleteMeetup(int id)
    {
        var meetup = _repository.GetMeetupById(id);
        if (meetup == null)
        {
            throw Exceptions.MeetupNotFound;
        }

        var username = _httpContext.User.Identity.Name;
        if (meetup.Organizer.Username != username)
        {
            throw Exceptions.NotYourMeetup;
        }

        _repository.DeleteMeetup(id);
        _repository.Save();
    }

    private bool AlreadyExists(string name)
    {
        return _repository.GetMeetupByName(name) != null;
    }
}
