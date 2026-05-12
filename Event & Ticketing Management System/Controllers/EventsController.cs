using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.DTOs.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _service;

        public EventsController(IEventService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("organizer/{organizerId}")]
        public async Task<IActionResult> GetByOrganizer(string organizerId)
        {
            return Ok(await _service.GetByOrganizerIdAsync(organizerId));
        }

        [Authorize(Roles = "Organizer")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateEventDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(new { EventId = id });
        }

        [Authorize(Roles = "Organizer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateEventDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

        [Authorize(Roles = "Organizer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
    }
}
