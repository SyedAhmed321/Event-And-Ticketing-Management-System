using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Reservations;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IMongoCollection<Reservation> _reservations;
        private readonly IMongoCollection<ReservationLock> _locks;

        public ReservationRepository(MongoDbService mongoDbService)
        {
            _reservations = mongoDbService.Database.GetCollection<Reservation>("Reservations");
            _locks = mongoDbService.Database.GetCollection<ReservationLock>("ReservationLocks");
        }

        //Create reservation
        public async Task CreateAsync(Reservation reservation)
        {
            await _reservations.InsertOneAsync(reservation);
        }

        //Get reservation by ID
        public async Task<Reservation?> GetByIdAsync(string reservationId)
        {
            return await _reservations
                .Find(r => r.Id == reservationId)
                .FirstOrDefaultAsync();
        }

        //Get user reservations
        public async Task<List<Reservation>> GetByUserIdAsync(string userId)
        {
            return await _reservations
                .Find(r => r.UserId == userId)
                .SortByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        //Update reservation
        public async Task UpdateAsync(Reservation reservation)
        {
            await _reservations.ReplaceOneAsync(
                r => r.Id == reservation.Id,
                reservation
            );
        }

        //Delete reservation
        public async Task DeleteAsync(string reservationId)
        {
            await _reservations.DeleteOneAsync(r => r.Id == reservationId);
        }

        //---------------- LOCKS ----------------

        //Insert locks
        public async Task InsertLocksAsync(List<ReservationLock> locks)
        {
            await _locks.InsertManyAsync(locks);
        }

        //Get all locks for event
        public async Task<List<ReservationLock>> GetLocksByEventAsync(string eventId)
        {
            return await _locks
                .Find(l => l.EventId == eventId)
                .ToListAsync();
        }

        //Get locks for specific ticket type
        public async Task<List<ReservationLock>> GetLocksByTicketTypeAsync(string eventId, string ticketTypeId)
        {
            return await _locks
                .Find(l => l.EventId == eventId && l.TicketTypeId == ticketTypeId)
                .ToListAsync();
        }

        //Remove expired locks
        public async Task RemoveExpiredLocksAsync()
        {
            var now = DateTime.UtcNow;

            await _locks.DeleteManyAsync(l => l.ExpiresAt < now);
        }

        //Remove locks by user
        public async Task RemoveLocksByUserAsync(string userId)
        {
            await _locks.DeleteManyAsync(l => l.UserId == userId);
        }
    }
}