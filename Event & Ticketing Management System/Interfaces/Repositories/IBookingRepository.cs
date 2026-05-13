using Event___Ticketing_Management_System.Models.Bookings;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Task CreateBookingAsync(Booking booking);
        Task<List<Booking>> GetBookingsByUserIdAsync(string userId);
        Task<List<Booking>> GetBookingsByEventIdAsync(string eventId);
        Task<Booking?> GetBookingByIdAsync(string bookingId);
        Task UpdateBookingStatusAsync(string bookingId, string status);
    }
}
