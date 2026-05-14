using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Tickets;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IMongoCollection<Ticket> _tickets;

        public TicketRepository(MongoDbService db)
        {
            _tickets = db.Database.GetCollection<Ticket>("Tickets");
        }

        public async Task CreateTicketAsync(Ticket ticket)
        {
            await _tickets.InsertOneAsync(ticket);
        }

        public async Task CreateManyTicketsAsync(List<Ticket> tickets)
        {
            await _tickets.InsertManyAsync(tickets);
        }

        public async Task<List<Ticket>> GetTicketsByUserIdAsync(string userId)
        {
            return await _tickets
                .Find(t => t.UserId == userId)
                .SortByDescending(t => t.IssuedAt)
                .ToListAsync();
        }

        public async Task<List<Ticket>> GetTicketsByBookingIdAsync(string bookingId)
        {
            return await _tickets
                .Find(t => t.BookingId == bookingId)
                .ToListAsync();
        }

        public async Task<List<Ticket>> GetTicketsByEventIdAsync(string eventId)
        {
            return await _tickets
                .Find(t => t.EventId == eventId)
                .SortByDescending(t => t.IssuedAt)
                .ToListAsync();
        }

        public async Task<Ticket?> GetTicketByIdAsync(string ticketId)
        {
            return await _tickets
                .Find(t => t.Id == ticketId)
                .FirstOrDefaultAsync();
        }

        public async Task<Ticket?> GetTicketByQRCodeAsync(string qrCode)
        {
            return await _tickets
                .Find(t => t.QRCode == qrCode)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateTicketStatusAsync(string ticketId, string status)
        {
            await _tickets.UpdateOneAsync(
                t => t.Id == ticketId,
                Builders<Ticket>.Update.Set(t => t.Status, status));
        }

        public async Task MarkTicketCheckedInAsync(string ticketId)
        {
            await _tickets.UpdateOneAsync(
                t => t.Id == ticketId,
                Builders<Ticket>.Update
                    .Set(t => t.CheckedInAt, true)
                    .Set(t => t.Status, "Used"));                    
        }

        public async Task<int> GetCheckedInCountAsync(string eventId)
        {
            var count = await _tickets.CountDocumentsAsync(
                t => t.EventId == eventId && t.CheckedInAt == true);
            return (int)count;
        }

        public async Task<int> GetTotalTicketsByEventIdAsync(string eventId)
        {
            var count = await _tickets.CountDocumentsAsync(
                t => t.EventId == eventId);
            return (int)count;
        }
    }
}