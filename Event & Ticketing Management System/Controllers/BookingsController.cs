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

        private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;


        // User books tickets
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            var userId = GetUserId();

            var bookingId = await _bookingService.CreateBookingAsync(userId, dto);
            return Ok(new
            {
                message = "Booking created successfully",
                bookingId
            });
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmBooking([FromBody] ConfirmBookingDto dto)
        {
            var userId = GetUserId();
            var result = await _bookingService.ConfirmBookingAsync(userId, dto);
            return Ok(new
            {
                message = result
            });
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelBooking([FromBody] CancelBookingDto dto)
        {
            var userId = GetUserId();
            var result = await _bookingService.CancelBookingAsync(userId, dto);
            return Ok(new
            {
                message = result
            });
        }

        // User views their booking history
        [HttpGet("my-bookings")]
        [Authorize]
        public async Task<IActionResult> GetMyBookings()
        {
            var userId = GetUserId();
            var result = await _bookingService.GetMyBookingsAsync(userId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(string id)
        {
            var result = await _bookingService.GetByIdAsync(id);
            if(result == null)
                return NotFound("booking not found");

            return Ok(result);
        }

    }
}
