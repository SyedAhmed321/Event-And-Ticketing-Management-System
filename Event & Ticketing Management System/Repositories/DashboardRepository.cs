using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Users;
using Event___Ticketing_Management_System.Models.Events;
using Event___Ticketing_Management_System.Models.Bookings;
using Event___Ticketing_Management_System.Models.Tickets;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Event> _events;
        private readonly IMongoCollection<Booking> _bookings;
        private readonly IMongoCollection<Ticket> _tickets;

        public DashboardRepository(MongoDbService mongoDbService)
        {
            _users = mongoDbService.Database.GetCollection<User>("Users");
            _events = mongoDbService.Database.GetCollection<Event>("Events");
            _bookings = mongoDbService.Database.GetCollection<Booking>("Bookings");
            _tickets = mongoDbService.Database.GetCollection<Ticket>("Tickets");
        }

        // =========================================
        // ✅ TOTAL USERS
        // =========================================
        public async Task<int> GetTotalUsersAsync()
        {
            return (int)await _users.CountDocumentsAsync(_ => true);
        }

        // =========================================
        // ✅ TOTAL EVENTS
        // =========================================
        public async Task<int> GetTotalEventsAsync()
        {
            return (int)await _events.CountDocumentsAsync(_ => true);
        }

        // =========================================
        // ✅ TOTAL BOOKINGS
        // =========================================
        public async Task<int> GetTotalBookingsAsync()
        {
            return (int)await _bookings.CountDocumentsAsync(_ => true);
        }

        // =========================================
        // ✅ TOTAL TICKETS SOLD
        // =========================================
        public async Task<int> GetTotalTicketsSoldAsync()
        {
            var tickets = await _tickets.Find(_ => true).ToListAsync();

            return tickets.Count;
        }

        // =========================================
        // ✅ TOTAL REVENUE
        // =========================================
        public async Task<decimal> GetTotalRevenueAsync()
        {
            var bookings = await _bookings.Find(b => b.Payment.PaymentStatus == "Paid")
                                          .ToListAsync();

            return bookings.Sum(b => b.TotalAmount);
        }

        // =========================================
        // ✅ REVENUE OVER TIME (OPTIONAL)
        // =========================================
        public async Task<List<(DateTime date, decimal revenue)>> GetRevenueByDateAsync()
        {
            var bookings = await _bookings
                .Find(b => b.Payment.PaymentStatus == "Paid")
                .ToListAsync();

            var data = bookings
                .GroupBy(b => b.BookingDate.Date)
                .Select(g => (date: g.Key, revenue: g.Sum(b => b.TotalAmount)))
                .OrderBy(x => x.date)
                .ToList();

            return data;
        }
    }
}