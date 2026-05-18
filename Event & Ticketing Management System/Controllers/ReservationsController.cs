using Event___Ticketing_Management_System.DTOs.Reservations;
using Event___Ticketing_Management_System.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] //all endpoints require authentication
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        //Helper method
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }

        // ===============================
        //CREATE RESERVATION
        // ===============================

        /// <summary>
        /// Create reservation (lock tickets for 5 minutes)
        /// POST: api/reservations
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
        {
            var userId = GetUserId();

            var reservationId = await _reservationService.CreateReservationAsync(userId, dto);

            return Ok(new
            {
                message = "Reservation created successfully",
                reservationId
            });
        }

        // ===============================
        //GET RESERVATION BY ID
        // ===============================

        /// <summary>
        /// Get reservation details
        /// GET: api/reservations/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(string id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);

            if (reservation == null)
                return NotFound("Reservation not found");

            return Ok(reservation);
        }

        // ===============================
        //CONFIRM RESERVATION
        // ===============================

        /// <summary>
        /// Confirm reservation and convert to booking
        /// POST: api/reservations/confirm
        /// </summary>
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmReservation([FromBody] ConfirmReservationDto dto)
        {
            var userId = GetUserId();

            var bookingId = await _reservationService.ConfirmReservationAsync(userId, dto);

            return Ok(new
            {
                message = "Reservation confirmed successfully",
                bookingId
            });
        }

        // ===============================
        //CANCEL RESERVATION
        // ===============================

        /// <summary>
        /// Cancel reservation
        /// POST: api/reservations/{id}/cancel
        /// </summary>
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelReservation(string id)
        {
            var userId = GetUserId();

            var result = await _reservationService.CancelReservationAsync(userId, id);

            return Ok(new
            {
                message = result
            });
        }

        // ===============================
        //CLEANUP EXPIRED (ADMIN / BACKGROUND)
        // ===============================

        /// <summary>
        /// Remove expired reservations (optional/manual call)
        /// POST: api/reservations/cleanup
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("cleanup")]
        public async Task<IActionResult> CleanupExpiredReservations()
        {
            await _reservationService.CleanupExpiredReservationsAsync();

            return Ok(new
            {
                message = "Expired reservations cleaned"
            });
        }
    }
}