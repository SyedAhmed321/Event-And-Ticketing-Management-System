using Event___Ticketing_Management_System.Models.Tickets;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface ITicketRepository
    {

        //Create multiple tickets (after booking confirm)
        Task InsertManyAsync(List<Ticket> tickets);

        //Get ticket by ID
        Task<Ticket?> GetByIdAsync(string ticketId);

        //Get ticket using QR code (VERY IMPORTANT)
        Task<Ticket?> GetByQRCodeAsync(string qrCode);

        //Get all tickets for a booking
        Task<List<Ticket>> GetByBookingIdAsync(string bookingId);

        //Get all tickets of a user
        Task<List<Ticket>> GetByUserIdAsync(string userId);

        //Update ticket (used in check-in, status change)
        Task UpdateAsync(Ticket ticket);

    }
}
