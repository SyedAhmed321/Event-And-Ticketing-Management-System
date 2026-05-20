using Event___Ticketing_Management_System.DTOs.Events;
using Event___Ticketing_Management_System.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route("api/organizer-events")]
    [Authorize(Roles = "Organizer")]
    public class OrganizerEventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public OrganizerEventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        private string GetUserId() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        // ✅ Create event (Organizer only)
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto dto)
        {
            var organizerId = GetUserId();

            var id = await _eventService.CreateAsync(dto, organizerId);

            return Ok(new { message = "Event created", eventId = id });
        }

        //Get my events
        [HttpGet("my")]
        public async Task<IActionResult> GetMyEvents()
        {
            var organizerId = GetUserId();

            var events = await _eventService.GetByOrganizerIdAsync(organizerId);

            return Ok(events);
        }
    }
}