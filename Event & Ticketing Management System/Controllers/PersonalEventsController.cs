using Event___Ticketing_Management_System.DTOs.PersonalEvents;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PersonalEventsController : ControllerBase
    {
        private readonly IPersonalEventService _service;
        private readonly IOrganizerRepository _organizerRepository;

        public PersonalEventsController(IPersonalEventService service, IOrganizerRepository organizerRepository)
        {
            _service = service;
            _organizerRepository = organizerRepository;
        }

        //Helper to get logged-in user
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }

        // =========================================
        //  CREATE PERSONAL EVENT
        // =========================================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonalEventDto dto)
        {
            var userId = GetUserId();

            var id = await _service.CreateEventAsync(userId, dto);

            return Ok(new
            {
                message = "Personal event created",
                eventId = id
            });
        }

        // =========================================
        //  GET MY EVENTS
        // =========================================
        [HttpGet("my")]
        public async Task<IActionResult> GetMyEvents()
        {
            var userId = GetUserId();

            var result = await _service.GetMyEventsAsync(userId);

            return Ok(result);
        }

        // =========================================
        //  GET EVENT BY ID
        // =========================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound("Event not found");

            return Ok(result);
        }

        // =========================================
        //  GET EVENT BY ORGANIZER ID
        // =========================================
        [Authorize(Roles = "Organizer")]
        [HttpGet("organizer")]
        public async Task<IActionResult> GetOrganizerEvents()
        {

            var userId = GetUserId();

            var organizer = await _organizerRepository.GetByUserIdAsync(userId);

            if (organizer == null)
            {
                return NotFound("Organizer profile not found");
            }

            var result = await _service.GetByOrganizerAsync(organizer.Id);

            return Ok(result);

        }



        // =========================================
        //  UPDATE EVENT
        // =========================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePersonalEventDto dto)
        {
            var userId = GetUserId();

            var result = await _service.UpdateEventAsync(userId, id, dto);

            return Ok(new
            {
                message = result
            });
        }

        // =========================================
        //  ADD REQUIREMENT
        // =========================================
        [HttpPost("{id}/requirements")]
        public async Task<IActionResult> AddRequirement(string id, [FromBody] CreateRequirementDto dto)
        {
            var userId = GetUserId();

            var result = await _service.AddRequirementAsync(userId, id, dto);

            return Ok(new
            {
                message = result
            });
        }

        // =========================================
        //  ADD TIMELINE
        // =========================================
        [HttpPost("{id}/timeline")]
        public async Task<IActionResult> AddTimeline(string id, [FromBody] AddTimelineDto dto)
        {
            var userId = GetUserId();

            var result = await _service.AddTimelineAsync(userId, id, dto);

            return Ok(new
            {
                message = result
            });
        }

        // =========================================
        //  UPDATE BUDGET
        // =========================================
        [HttpPut("{id}/budget")]
        public async Task<IActionResult> UpdateBudget(string id, [FromBody] UpdateBudgetDto dto)
        {
            var userId = GetUserId();

            var result = await _service.UpdateBudgetAsync(userId, id, dto);

            return Ok(new
            {
                message = result
            });
        }

        // =========================================
        //  DELETE EVENT
        // =========================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = GetUserId();

            var result = await _service.DeleteEventAsync(userId, id);

            return Ok(new
            {
                message = result
            });
        }
    }
}