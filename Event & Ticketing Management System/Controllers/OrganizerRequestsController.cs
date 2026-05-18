using Event___Ticketing_Management_System.DTOs.Organizers;
using Event___Ticketing_Management_System.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route("api/organizer-requests")]
    public class OrganizerRequestController : ControllerBase
    {
        private readonly IOrganizerService _organizerService;

        public OrganizerRequestController(IOrganizerService organizerService)
        {
            _organizerService = organizerService;
        }

        private string GetUserId() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        //USER: Create request
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrganizerRequestDto dto)
        {
            var userId = GetUserId();

            var id = await _organizerService.CreateRequestAsync(userId, dto);

            return Ok(new { message = "Request sent", requestId = id });
        }

        //USER: My requests
        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetMy()
        {
            var userId = GetUserId();

            var result = await _organizerService.GetMyRequestsAsync(userId);

            return Ok(result);
        }

        //ORGANIZER: Requests for me
        [Authorize(Roles = "Organizer")]
        [HttpGet]
        public async Task<IActionResult> GetForOrganizer()
        {
            var userId = GetUserId();

            var result = await _organizerService.GetRequestsForOrganizerAsync(userId);  

            return Ok(result);
        }

        //ORGANIZER: Accept / Reject
        [Authorize(Roles = "Organizer")]
        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateOrganizerRequestStatusDto dto)
        {
            var userId = GetUserId();

            var msg = await _organizerService.UpdateRequestStatusAsync(userId, dto);

            return Ok(new { message = msg });
        }
    }
}