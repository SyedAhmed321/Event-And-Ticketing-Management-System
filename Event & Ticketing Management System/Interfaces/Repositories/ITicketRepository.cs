using Event___Ticketing_Management_System.Models.Tickets;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task CreateTicketAsync(Ticket ticket);
        Task CreateManyTicketsAsync(List<Ticket> tickets);
        Task<List<Ticket>> GetTicketsByUserIdAsync(string userId);
        Task<List<Ticket>> GetTicketsByBookingIdAsync(string bookingId);
        Task<Ticket?> GetTicketByQRCodeAsync(string qrCode);
        Task<Ticket?> GetTicketByIdAsync(string ticketId);
        Task UpdateTicketStatusAsync(string ticketId, string status);
        Task MarkTicketCheckedInAsync(string ticketId);
    }
}
