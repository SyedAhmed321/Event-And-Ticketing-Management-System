using Event___Ticketing_Management_System.DTOs.Bookings;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // User books tickets
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userName = User.FindFirst(ClaimTypes.Name)!.Value;

            var result = await _bookingService.CreateBookingAsync(userId, userName, dto);
            return Ok(result);
        }

        // User views their booking history
        [HttpGet("my-bookings")]
        [Authorize]
        public async Task<IActionResult> GetMyBookings()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _bookingService.GetMyBookingsAsync(userId);
            return Ok(result);
        }

        // Organizer views bookings for their event
        [HttpGet("event/{eventId}")]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> GetEventBookings(string eventId)
        {
            var result = await _bookingService.GetEventBookingsAsync(eventId);
            return Ok(result);
        }

        // User cancels their booking
        [HttpPatch("{bookingId}/cancel")]
        [Authorize]
        public async Task<IActionResult> CancelBooking(string bookingId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            await _bookingService.CancelBookingAsync(bookingId, userId);
            return Ok(new { message = "Booking cancelled successfully" });
        }

        // Organizer scans QR code at gate
        [HttpPost("validate")]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> ValidateTicket([FromQuery] string qrCode)
        {
            var result = await _bookingService.ValidateTicketAsync(qrCode);
            return Ok(result);
        }
    }
}
