using Event___Ticketing_Management_System.DTOs.Bookings;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface IBookingService
    {

        Task<string> CreateBookingAsync(string userId, CreateBookingDto dto);

        // ✅ Confirm booking (payment step)
        Task<string> ConfirmBookingAsync(string userId, ConfirmBookingDto dto);

        // ✅ Cancel booking
        Task<string> CancelBookingAsync(string userId, CancelBookingDto dto);

        // ✅ Get user bookings
        Task<List<BookingResponseDto>> GetMyBookingsAsync(string userId);

        // ✅ Get booking by ID
        Task<BookingResponseDto?> GetByIdAsync(string bookingId);

    }
}
