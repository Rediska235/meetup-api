using MeetupAPI.Application.DTOs;
using MeetupAPI.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetMeetups()
    {
        return Ok(_service.GetMeetups());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetMeetupById(int id)
    {
        return Ok(_service.GetMeetupById(id));
    }

    [HttpPost]
    public IActionResult CreateMeetup(CreateMeetupDto createMeetupDto)
    {
        var product = _service.CreateMeetup(createMeetupDto);
        return Created($"/api/meetups/{product.Id}", product);
    }

    [HttpPut]
    public IActionResult UpdateMeetup(UpdateMeetupDto updateMeetupDto)
    {
        var product = _service.UpdateMeetup(updateMeetupDto);
        return Ok(product);
    }

    [HttpDelete("{id:guid}"), Authorize]
    public IActionResult DeleteMeetup(int id)
    {
        _service.DeleteMeetup(id);
        return NoContent();
    }
}
