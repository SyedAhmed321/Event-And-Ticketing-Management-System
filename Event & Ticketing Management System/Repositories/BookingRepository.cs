using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Bookings;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IMongoCollection<Booking> _bookings;

        public BookingRepository(MongoDbService mongoDbService)
        {
            _bookings = mongoDbService.Database.GetCollection<Booking>("Bookings");
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            await _bookings.InsertOneAsync(booking);
        }

        public async Task<List<Booking>> GetBookingsByUserIdAsync(string userId)
        {
            return await _bookings
                .Find(b => b.UserId == userId)
                .SortByDescending(b => b.BookingDate)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByEventIdAsync(string eventId)
        {
            return await _bookings
                .Find(b => b.EventId == eventId)
                .SortByDescending(b => b.BookingDate)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(string bookingId)
        {
            return await _bookings
                .Find(b => b.Id == bookingId)
                .FirstOrDefaultAsync();
        }


        public async Task UpdateAsync(Booking booking)
        {
            await _bookings.ReplaceOneAsync(
                b => b.Id == booking.Id,
                booking
            );
        }



        public async Task DeleteBookingAsync(string bookingId)
        {
            await _bookings.DeleteOneAsync(b => b.Id == bookingId);
        }

    }
}
