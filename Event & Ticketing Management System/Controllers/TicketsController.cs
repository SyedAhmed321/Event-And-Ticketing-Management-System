using Event___Ticketing_Management_System.DTOs.Tickets;
using Event___Ticketing_Management_System.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event___Ticketing_Management_System.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // ✅ All endpoints require login
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // ✅ Helper to get logged-in user ID
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }

        // ===============================
        // ✅ GET MY TICKETS
        // ===============================

        /// <summary>
        /// Get logged-in user's tickets
        /// GET: api/tickets/my-tickets
        /// </summary>
        [HttpGet("my-tickets")]
        public async Task<IActionResult> GetMyTickets()
        {
            var userId = GetUserId();

            var tickets = await _ticketService.GetMyTicketsAsync(userId);

            return Ok(tickets);
        }

        // ===============================
        // ✅ VALIDATE TICKET (QR SCAN)
        // ===============================

        /// <summary>
        /// Validate ticket using QR code
        /// POST: api/tickets/validate
        /// </summary>
        [HttpPost("validate")]
        public async Task<IActionResult> ValidateTicket([FromBody] ValidateTicketDto dto)
        {
            var result = await _ticketService.ValidateTicketAsync(dto);

            return Ok(result);
        }

        // ===============================
        // ✅ CHECK-IN TICKET
        // ===============================

        /// <summary>
        /// Check-in ticket (used by staff/admin)
        /// POST: api/tickets/check-in
        /// </summary>
        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost("check-in")]
        public async Task<IActionResult> CheckInTicket([FromBody] CheckInDto dto)
        {
            var staffUserId = GetUserId();

            var result = await _ticketService.CheckInTicketAsync(dto, staffUserId);

            return Ok(new
            {
                message = result
            });
        }
    }

}