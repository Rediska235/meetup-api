using MeetupAPI.Application.DTOs;
using MeetupAPI.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MeetupAPI.Presentation.Controllers;

[ApiController]
[Route("/api/meetups")]
public class MeetupController : ControllerBase
{
    private readonly IMeetupService _service;

    public MeetupController(IMeetupService service)
    {
        _service = service;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all meetups")]
    public IActionResult GetMeetups()   
    {
        return Ok(_service.GetMeetups());
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get meetup by id")]
    public IActionResult GetMeetupById(int id)
    {
        return Ok(_service.GetMeetupById(id));
    }

    [HttpPost, Authorize]
    [SwaggerOperation(Summary = "Create meetup")]
    public IActionResult CreateMeetup(CreateMeetupDto createMeetupDto)
    {
        var meetup = _service.CreateMeetup(createMeetupDto);
        return Created($"/api/meetups/id", meetup);
    }

    [HttpPut, Authorize]
    [SwaggerOperation(Summary = "Update meetup")]
    public IActionResult UpdateMeetup(UpdateMeetupDto updateMeetupDto)
    {
        var meetup = _service.UpdateMeetup(updateMeetupDto);
        return Ok(meetup);
    }

    [HttpDelete("{id:int}"), Authorize]
    [SwaggerOperation(Summary = "Delete meetup by id")]
    public IActionResult DeleteMeetup(int id)
    {
        _service.DeleteMeetup(id);
        return NoContent();
    }
}
