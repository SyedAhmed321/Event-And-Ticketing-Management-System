using Event___Ticketing_Management_System.DTOs.Reservations;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface IReservationService
    {
        //Create reservation (hold tickets)
        Task<string> CreateReservationAsync(string userId, CreateReservationDto dto);

        //Get reservation by ID
        Task<ReservationResponseDto?> GetByIdAsync(string reservationId);

        //Confirm reservation → convert to booking
        Task<string> ConfirmReservationAsync(string userId, ConfirmReservationDto dto);

        //Cancel or expire reservation
        Task<string> CancelReservationAsync(string userId, string reservationId);

        //Cleanup expired reservations (background)
        Task CleanupExpiredReservationsAsync();
    }
}