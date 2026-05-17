using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Tickets;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{

    public class TicketRepository : ITicketRepository
    {
        private readonly IMongoCollection<Ticket> _tickets;

        public TicketRepository(MongoDbService mongoDbService)
        {
            _tickets = mongoDbService.Database.GetCollection<Ticket>("Tickets");
        }

        //Insert multiple tickets
        public async Task InsertManyAsync(List<Ticket> tickets)
        {
            await _tickets.InsertManyAsync(tickets);
        }

        //Get by ticket ID
        public async Task<Ticket?> GetByIdAsync(string ticketId)
        {
            return await _tickets
                .Find(t => t.Id == ticketId)
                .FirstOrDefaultAsync();
        }

        //Get ticket by QR code
        public async Task<Ticket?> GetByQRCodeAsync(string qrCode)
        {
            return await _tickets
                .Find(t => t.QRCode == qrCode)
                .FirstOrDefaultAsync();
        }

        //Get tickets by booking
        public async Task<List<Ticket>> GetByBookingIdAsync(string bookingId)
        {
            return await _tickets
                .Find(t => t.BookingId == bookingId)
                .ToListAsync();
        }

        //Get tickets by user
        public async Task<List<Ticket>> GetByUserIdAsync(string userId)
        {
            return await _tickets
                .Find(t => t.UserId == userId)
                .SortByDescending(t => t.IssuedAt)
                .ToListAsync();
        }

        //Update ticket (used for check-in & status change)
        public async Task UpdateAsync(Ticket ticket)
        {
            await _tickets.ReplaceOneAsync(
                t => t.Id == ticket.Id,
                ticket
            );
        }
    }

}