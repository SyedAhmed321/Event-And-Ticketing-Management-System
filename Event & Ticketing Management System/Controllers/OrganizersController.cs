using Event___Ticketing_Management_System.DTOs.Organizers;
using Event___Ticketing_Management_System.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route("api/organizers")]
    public class OrganizersController : ControllerBase
    {
        private readonly IOrganizerService _organizerService;

        public OrganizersController(IOrganizerService organizerService)
        {
            _organizerService = organizerService;
        }

        private string GetUserId() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        //Get all organizers
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _organizerService.GetAllOrganizersAsync();
            return Ok(result);
        }

        //Get my organizer profile
        [Authorize(Roles = "Organizer")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMy()
        {
            var userId = GetUserId();
            var result = await _organizerService.GetMyOrganizerAsync(userId);

            if (result == null) return NotFound("Not found");

            return Ok(result);
        }

        //Update profile
        [Authorize(Roles = "Organizer")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrganizerDto dto)
        {
            var userId = GetUserId();
            var msg = await _organizerService.UpdateOrganizerAsync(userId, dto);

            return Ok(new { message = msg });
        }

        //Add rating
        [Authorize]
        [HttpPost("rate")]
        public async Task<IActionResult> Rate([FromBody] OrganizerRatingDto dto)
        {
            var userId = GetUserId();
            var msg = await _organizerService.AddRatingAsync(userId, dto);

            return Ok(new { message = msg });
        }
    }
}