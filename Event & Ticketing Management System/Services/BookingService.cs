using Event___Ticketing_Management_System.DTOs.Bookings;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Bookings;
using Event___Ticketing_Management_System.Utilities;

namespace Event___Ticketing_Management_System.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ITicketService _ticketService;
        private readonly IReservationRepository _reservationRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationService _notificationService;

        public BookingService(
            IBookingRepository bookingRepository,
            IEventRepository eventRepository,
            ITicketService ticketService,
            IReservationRepository reservationRepository,
            ITicketRepository ticketRepository,
            INotificationRepository notificationRepository,
            INotificationService notificationService)
        {
            _bookingRepository = bookingRepository;
            _eventRepository = eventRepository;
            _ticketService = ticketService;
            _reservationRepository = reservationRepository;
            _ticketRepository = ticketRepository;
            _notificationRepository = notificationRepository;
            _notificationService = notificationService;
        }

        // ✅ CREATE BOOKING
        public async Task<string> CreateBookingAsync(string userId, CreateBookingDto dto)
        {
            var ev = await _eventRepository.GetByIdAsync(dto.EventId);

            if (ev == null)
                throw new Exception("Event not found");

            // ✅ Fix: remove expired locks first
            await _reservationRepository.RemoveExpiredLocksAsync();

            var bookingItems = new List<BookingItem>();
            decimal totalAmount = 0;

            foreach (var item in dto.Items)
            {
                var ticket = ev.TicketTypes.FirstOrDefault(t => t.Id == item.TicketTypeId);

                if (ticket == null)
                    throw new Exception($"Ticket type not found: {item.TicketTypeId}");

                // ✅ Reservation-aware availability
                var locks = await _reservationRepository
                    .GetLocksByTicketTypeAsync(ev.Id, ticket.Id);

                var lockedQty = locks.Sum(l => l.LockedQuantity);

                var available = ticket.Quantity - ticket.Sold - lockedQty;

                if (item.Quantity > available)
                    throw new Exception($"Not enough tickets available for {ticket.Name}");

                bookingItems.Add(new BookingItem
                {
                    TicketTypeId = ticket.Id,
                    TicketTypeName = ticket.Name,
                    Quantity = item.Quantity,
                    UnitPrice = ticket.Price
                });

                totalAmount += item.Quantity * ticket.Price;
            }

            var booking = new Booking
            {
                UserId = userId,
                EventId = dto.EventId,
                Items = bookingItems,
                TotalAmount = totalAmount,
                BookingStatus = BookingStatusConstants.Pending,
                Payment = new PaymentInfo
                {
                    PaymentStatus = PaymentStatusConstatants.Pending
                },
                BookingDate = DateTime.UtcNow
            };

            await _bookingRepository.CreateBookingAsync(booking);

            return booking.Id;
        }

        // ✅ CONFIRM BOOKING
        public async Task<string> ConfirmBookingAsync(string userId, ConfirmBookingDto dto)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(dto.BookingId);

            if (booking == null)
                throw new Exception("Booking not found");

            if (booking.UserId != userId)
                throw new UnauthorizedAccessException("Unauthorized");

            if (booking.BookingStatus != BookingStatusConstants.Pending)
                throw new Exception("Booking already processed");

            // ✅ Payment validation
            if (string.IsNullOrWhiteSpace(dto.PaymentMethod))
                throw new Exception("Payment method required");

            if (!PaymentMethodConstants.GetAll().Contains(dto.PaymentMethod))
                throw new Exception("Invalid payment method");

            var ev = await _eventRepository.GetByIdAsync(booking.EventId);
            if (ev == null)
                throw new Exception("Event not found");

            // ✅ Prevent duplicate ticket generation
            var existingTickets = await _ticketRepository.GetByBookingIdAsync(booking.Id);
            if (existingTickets.Any())
                throw new Exception("Tickets already generated");

            // ✅ Deduct tickets
            foreach (var item in booking.Items)
            {
                var ticket = ev.TicketTypes.First(t => t.Id == item.TicketTypeId);

                var available = ticket.Quantity - ticket.Sold;

                if (item.Quantity > available)
                    throw new Exception($"Not enough tickets available for {ticket.Name}");

                ticket.Sold += item.Quantity;
                ev.TotalTicketsSold += item.Quantity;
            }

            await _eventRepository.UpdateAsync(ev);

            // ✅ Fix: payment null safety
            if (booking.Payment == null)
                booking.Payment = new PaymentInfo();

            booking.BookingStatus = BookingStatusConstants.Confirmed;

            booking.Payment.PaymentMethod = dto.PaymentMethod;
            booking.Payment.PaymentStatus = PaymentStatusConstatants.Paid;
            booking.Payment.PaidAt = DateTime.UtcNow;

            booking.UpdatedAt = DateTime.UtcNow;

            await _bookingRepository.UpdateAsync(booking);

            // ✅ Generate tickets AFTER successful booking update
            await _ticketService.GenerateTicketsAsync(booking.Id, booking.UserId);

            await _notificationService.SendAsync(
                userId,
                "Booking Confirmed",
                "Your booking has been confirmed",
                "Booking"
            );

            return "Booking Confirmed Successfully";
        }

        // ✅ CANCEL BOOKING
        public async Task<string> CancelBookingAsync(string userId, CancelBookingDto dto)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(dto.BookingId);

            if (booking == null)
                throw new Exception("Booking not found");

            if (booking.UserId != userId)
                throw new UnauthorizedAccessException("Unauthorized");

            if (booking.BookingStatus == BookingStatusConstants.Cancelled)
                throw new Exception("Booking already cancelled");

            var ev = await _eventRepository.GetByIdAsync(booking.EventId);

            if (ev == null)
                throw new Exception("Event not found");

            foreach (var item in booking.Items)
            {
                var ticket = ev.TicketTypes.First(t => t.Id == item.TicketTypeId);

                ticket.Sold -= item.Quantity;
                ev.TotalTicketsSold -= item.Quantity; // ✅ FIX
            }

            await _eventRepository.UpdateAsync(ev);

            booking.BookingStatus = BookingStatusConstants.Cancelled;

            // ✅ Fix: payment null safety
            if (booking.Payment == null)
                booking.Payment = new PaymentInfo();

            booking.Payment.PaymentStatus = PaymentStatusConstatants.Refunded;

            booking.UpdatedAt = DateTime.UtcNow;

            await _bookingRepository.UpdateAsync(booking);

            return "Booking Cancelled Successfully";
        }

        // ✅ GET USER BOOKINGS
        public async Task<List<BookingResponseDto>> GetMyBookingsAsync(string userId)
        {
            var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId);
            return bookings.Select(MapToDto).ToList();
        }

        public async Task<BookingResponseDto?> GetByIdAsync(string bookingId)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            return booking == null ? null : MapToDto(booking);
        }

        // ✅ MAPPING
        private static BookingResponseDto MapToDto(Booking b)
        {
            return new BookingResponseDto
            {
                Id = b.Id,
                EventId = b.EventId,
                TotalAmount = b.TotalAmount,
                BookingStatus = b.BookingStatus,
                PaymentStatus = b.Payment?.PaymentStatus ?? "Unknown",
                BookingDate = b.BookingDate,

                Items = b.Items.Select(i => new BookingItemResponseDto
                {
                    TicketTypeName = i.TicketTypeName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            };
        }
    }
}