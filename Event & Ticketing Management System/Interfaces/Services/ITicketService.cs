using Event___Ticketing_Management_System.DTOs.Tickets;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface ITicketService
    {
        // User gets all their tickets
        Task<List<TicketResponseDto>> GetMyTicketsAsync(string userId);

        // User gets a single ticket by ID
        Task<TicketResponseDto> GetTicketByIdAsync(string ticketId, string userId);

        // User gets all tickets for a specific booking
        Task<List<TicketResponseDto>> GetTicketsByBookingIdAsync(string bookingId, string userId);

        // Organizer gets all tickets for their event
        Task<List<TicketResponseDto>> GetTicketsByEventIdAsync(string eventId);

        // Organizer scans QR at gate
        Task<ValidateTicketResponseDto> ValidateAndCheckInAsync(string qrCode);

        // Get attendance count for an event
        Task<object> GetAttendanceAsync(string eventId);
    }
}