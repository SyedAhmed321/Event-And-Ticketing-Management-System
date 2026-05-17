using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.DTOs.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Event___Ticketing_Management_System.Utilities;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Organizer")]
public class EventsController : ControllerBase
{
    private readonly IEventService _service;

    public EventsController(IEventService service)
    {
        _service = service;
    }

    private string GetUserId()
    {
        return User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;
    }

    // ✅ PUBLIC
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    // ✅ ORGANIZER - own events only
    [Authorize(Roles = RoleConstants.Organizer)]
    [HttpGet("my-events")]
    public async Task<IActionResult> GetMyEvents()
    {
        var userId = GetUserId();
        return Ok(await _service.GetByOrganizerIdAsync(userId));
    }

    // ✅ CREATE
    [Authorize(Roles = RoleConstants.Organizer)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateEventDto dto)
    {
        var userId = GetUserId();

        var id = await _service.CreateAsync(dto, userId);

        return Ok(new { EventId = id });
    }

    // ✅ UPDATE (owner only)
    [Authorize(Roles = RoleConstants.Organizer)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateEventDto dto)
    {
        var userId = GetUserId();

        var result = await _service.UpdateAsync(id, userId, dto);

        return Ok(result);
    }

    // ✅ DELETE (owner only)
    [Authorize(Roles = RoleConstants.Organizer)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var userId = GetUserId();

        var result = await _service.DeleteAsync(id, userId);

        return Ok(result);
    }
}

}
