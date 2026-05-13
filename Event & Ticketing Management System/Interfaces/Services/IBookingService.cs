using Event___Ticketing_Management_System.DTOs.Bookings;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface IBookingService
    {
        Task<BookingResponseDto> CreateBookingAsync(string userId, string userName, CreateBookingDto dto);
        Task<List<BookingResponseDto>> GetMyBookingsAsync(string userId);
        Task<List<BookingResponseDto>> GetEventBookingsAsync(string eventId);
        Task CancelBookingAsync(string bookingId, string userId);
        Task<object> ValidateTicketAsync(string qrCode);
    }
}
