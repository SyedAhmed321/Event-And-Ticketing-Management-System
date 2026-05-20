using Event___Ticketing_Management_System.DTOs.Bookings;
using Event___Ticketing_Management_System.DTOs.Reservations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Reservations;
using Event___Ticketing_Management_System.Utilities;

namespace Event___Ticketing_Management_System.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notificationService;

        public ReservationService(
            IReservationRepository reservationRepository,
            IEventRepository eventRepository,
            IBookingService bookingService,
            INotificationService notificationService)
        {
            _reservationRepository = reservationRepository;
            _eventRepository = eventRepository;
            _bookingService = bookingService;
            _notificationService = notificationService;
        }

        //CREATE RESERVATION
        public async Task<string> CreateReservationAsync(string userId, CreateReservationDto dto)
        {
            var ev = await _eventRepository.GetByIdAsync(dto.EventId);

            if (ev == null)
                throw new Exception("Event not found");

            //Remove expired locks first
            await _reservationRepository.RemoveExpiredLocksAsync();

            var reservationItems = new List<ReservationItem>();
            var locks = new List<ReservationLock>();

            foreach (var item in dto.Items)
            {
                var ticket = ev.TicketTypes.FirstOrDefault(t => t.Id == item.TicketTypeId);

                if (ticket == null)
                    throw new Exception("Ticket type not found");

                //Get locked tickets
                var locked = await _reservationRepository
                    .GetLocksByTicketTypeAsync(ev.Id, ticket.Id);

                var lockedQty = locked.Sum(l => l.LockedQuantity);

                var available = ticket.Quantity - ticket.Sold - lockedQty;

                if (item.Quantity > available)
                    throw new Exception($"Not enough tickets for {ticket.Name}");

                //Add reservation item
                reservationItems.Add(new ReservationItem
                {
                    TicketTypeId = ticket.Id,
                    TicketTypeName = ticket.Name,
                    Quantity = item.Quantity
                });

                //Create lock
                locks.Add(new ReservationLock
                {
                    EventId = ev.Id,
                    TicketTypeId = ticket.Id,
                    LockedQuantity = item.Quantity,
                    UserId = userId,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(5)
                });
            }

            var reservation = new Reservation
            {
                UserId = userId,
                EventId = dto.EventId,
                Items = reservationItems,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                Status = ReservationStatuses.Active,
                CreatedAt = DateTime.UtcNow
            };

            await _reservationRepository.CreateAsync(reservation);
            await _reservationRepository.InsertLocksAsync(locks);

            await _notificationService.SendAsync(
                userId,
                "Reservation Created",
                "You have 5 minutes to confirm booking",
                "Reservation"
            );

            return reservation.Id;
        }

        //GET RESERVATION
        public async Task<ReservationResponseDto?> GetByIdAsync(string reservationId)
        {
            var res = await _reservationRepository.GetByIdAsync(reservationId);

            if (res == null)
                return null;

            return new ReservationResponseDto
            {
                Id = res.Id,
                EventId = res.EventId,
                Status = res.Status,
                CreatedAt = res.CreatedAt,
                ExpiresAt = res.ExpiresAt,
                Items = res.Items.Select(i => new ReservationItemDetailsDto
                {
                    TicketTypeId = i.TicketTypeId,
                    TicketTypeName = i.TicketTypeName,
                    Quantity = i.Quantity
                }).ToList()
            };
        }

        //CONFIRM RESERVATION → BOOKING
        public async Task<string> ConfirmReservationAsync(string userId, ConfirmReservationDto dto)
        {
            var res = await _reservationRepository.GetByIdAsync(dto.ReservationId);

            if (res == null)
                throw new Exception("Reservation not found");

            if (res.UserId != userId)
                throw new UnauthorizedAccessException("Unauthorized");

            if (res.Status != ReservationStatuses.Active)
                throw new Exception("Reservation expired or already processed");

            if (res.ExpiresAt < DateTime.UtcNow)
                throw new Exception("Reservation expired");

            //Convert to booking
            var bookingDto = new CreateBookingDto
            {
                EventId = res.EventId,
                Items = res.Items.Select(i => new BookingItemDto
                {
                    TicketTypeId = i.TicketTypeId,
                    Quantity = i.Quantity
                }).ToList()
            };

            var bookingId = await _bookingService.CreateBookingAsync(userId, bookingDto);

            await _bookingService.ConfirmBookingAsync(userId, new ConfirmBookingDto
            {
                BookingId = bookingId,
                PaymentMethod = dto.PaymentMethod
            });

            //Mark reservation confirmed
            res.Status = ReservationStatuses.Confirmed;

            await _reservationRepository.UpdateAsync(res);

            //Remove locks
            await _reservationRepository.RemoveLocksByUserAsync(userId);

            return bookingId;
        }

        //CANCEL RESERVATION
        public async Task<string> CancelReservationAsync(string userId, string reservationId)
        {
            var res = await _reservationRepository.GetByIdAsync(reservationId);

            if (res == null)
                throw new Exception("Reservation not found");

            if (res.UserId != userId)
                throw new UnauthorizedAccessException("Unauthorized");

            res.Status = ReservationStatuses.Expired;

            await _reservationRepository.UpdateAsync(res);

            await _reservationRepository.RemoveLocksByUserAsync(userId);

            return "Reservation cancelled";
        }

        //CLEANUP EXPIRED (IMPORTANT)
        public async Task CleanupExpiredReservationsAsync()
        {
            await _reservationRepository.RemoveExpiredLocksAsync();
        }
    }
}