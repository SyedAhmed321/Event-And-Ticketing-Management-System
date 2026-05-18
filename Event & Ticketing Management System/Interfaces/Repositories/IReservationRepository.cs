using Event___Ticketing_Management_System.Models.Reservations;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        //Create reservation
        Task CreateAsync(Reservation reservation);

        //Get by ID
        Task<Reservation?> GetByIdAsync(string reservationId);

        //Get reservations by user
        Task<List<Reservation>> GetByUserIdAsync(string userId);

        //Update reservation (status, expiry etc.)
        Task UpdateAsync(Reservation reservation);

        //Delete reservation
        Task DeleteAsync(string reservationId);

        //-------- LOCKS (VERY IMPORTANT) --------

        //Add reservation locks
        Task InsertLocksAsync(List<ReservationLock> locks);

        //Get locks by event & ticket type
        Task<List<ReservationLock>> GetLocksByEventAsync(string eventId);

        //Get locks for specific ticket type
        Task<List<ReservationLock>> GetLocksByTicketTypeAsync(string eventId, string ticketTypeId);

        //Delete expired locks
        Task RemoveExpiredLocksAsync();

        //Remove locks by reservation/user
        Task RemoveLocksByUserAsync(string userId);
    }
}