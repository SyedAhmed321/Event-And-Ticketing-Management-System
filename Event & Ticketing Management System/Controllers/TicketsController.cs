using Event___Ticketing_Management_System.DTOs.Tickets;
using Event___Ticketing_Management_System.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET /api/tickets/my-tickets
        // User sees all their tickets
        [HttpGet("my-tickets")]
        public async Task<IActionResult> GetMyTickets()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _ticketService.GetMyTicketsAsync(userId);
            return Ok(result);
        }

        // GET /api/tickets/{ticketId}
        // User sees single ticket with QR code
        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetTicket(string ticketId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _ticketService.GetTicketByIdAsync(ticketId, userId);
            return Ok(result);
        }

        // GET /api/tickets/booking/{bookingId}
        // User sees all tickets for a specific booking
        [HttpGet("booking/{bookingId}")]
        public async Task<IActionResult> GetTicketsByBooking(string bookingId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _ticketService
                .GetTicketsByBookingIdAsync(bookingId, userId);
            return Ok(result);
        }

        // GET /api/tickets/event/{eventId}
        // Organizer sees all tickets for their event
        [HttpGet("event/{eventId}")]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> GetTicketsByEvent(string eventId)
        {
            var result = await _ticketService.GetTicketsByEventIdAsync(eventId);
            return Ok(result);
        }

        // POST /api/tickets/validate?qrCode=xxxxx
        // Organizer scans QR code at gate
        [HttpPost("validate")]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> ValidateTicket([FromQuery] string qrCode)
        {
            if (string.IsNullOrEmpty(qrCode))
                return BadRequest(new { message = "QR code is required" });

            var result = await _ticketService.ValidateAndCheckInAsync(qrCode);
            return Ok(result);
        }

        // GET /api/tickets/attendance/{eventId}
        // Organizer sees attendance stats
        [HttpGet("attendance/{eventId}")]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> GetAttendance(string eventId)
        {
            var result = await _ticketService.GetAttendanceAsync(eventId);
            return Ok(result);
        }

       
    }
}