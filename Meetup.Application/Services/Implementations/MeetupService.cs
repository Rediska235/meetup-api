using AutoMapper;
using MeetupAPI.Application.DTOs;
using MeetupAPI.Application.Repositories;
using MeetupAPI.Application.Services.Interfaces;
using MeetupAPI.Domain.Entities;
using MeetupAPI.Domain.Exceptions;

namespace MeetupAPI.Application.Services.Implementations;

public class MeetupService : IMeetupService
{
    private readonly IMeetupRepository _repository;
    private readonly IMapper _mapper;

    public MeetupService(IMeetupRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IEnumerable<Meetup> GetMeetups()
    {
        return _repository.GetMeetups();
    }

    public Meetup GetMeetupById(int id)
    {
        return _repository.GetMeetupById(id);
    }

    public Meetup CreateMeetup(CreateMeetupDto createMeetupDto)
    {
        var meetupName = createMeetupDto.Name.Trim();
        if (AlreadyExists(meetupName))
        {
            throw Exceptions.MeetupAlreadyExists;
        }

        var meetup = _mapper.Map<Meetup>(createMeetupDto);
        //meetup.Organizer = текущий пользователь

        _repository.InsertMeetup(meetup);
        _repository.Save();

        return meetup;
    }

    public Meetup UpdateMeetup(UpdateMeetupDto updateMeetupDto)
    {
        var meetup = _repository.GetMeetupById(updateMeetupDto.Id);

        var meetupName = updateMeetupDto.Name.Trim();
        if (meetupName != meetup.Name && AlreadyExists(meetupName))
        {
            throw Exceptions.MeetupAlreadyExists;
        }

        meetup = _mapper.Map<Meetup>(updateMeetupDto);
        //meetup.Organizer = текущий пользователь

        _repository.UpdateMeetup(meetup);
        _repository.Save();

        return meetup;
    }

    public void DeleteMeetup(int id)
    {
        //проверка на то, что это митап именно этого пользователя
        _repository.DeleteMeetup(id);
        _repository.Save();
    }

    private bool AlreadyExists(string name)
    {
        return _repository.GetMeetupByName(name) != null;
    }
}
