using Event___Ticketing_Management_System.DTOs.Bookings;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Bookings;
using Event___Ticketing_Management_System.Models.Tickets;
using Event___Ticketing_Management_System.Helpers;
using Event___Ticketing_Management_System.Utilities;

namespace Event___Ticketing_Management_System.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ITicketService _ticketService;

        public BookingService(
            IBookingRepository bookingRepository,
            IEventRepository eventRepository,
            ITicketService ticketService
            )
        {
            _bookingRepository = bookingRepository;
            _eventRepository = eventRepository;
            _ticketService = ticketService;
        }

        public async Task<string> CreateBookingAsync(string userId, CreateBookingDto dto)
        {

            var ev = await _eventRepository.GetByIdAsync(dto.EventId);

            if (ev == null)
                throw new Exception("Event not found");

            var bookingItems = new List<BookingItem>();

            decimal totalAmount = 0;

            foreach (var item in dto.Items)
            {
                var ticket = ev.TicketTypes
                    .FirstOrDefault(t => t.Id == item.TicketTypeId);

                if (ticket == null)
                    throw new Exception($"Ticket type not found: {item.TicketTypeId}");

                //Check availability
                var available = ticket.Quantity - ticket.Sold;

                if (item.Quantity > available)
                    throw new Exception($"Not enough tickets available for {ticket.Name}");

                //Build booking item
                var bookingItem = new BookingItem
                {
                    TicketTypeId = ticket.Id,
                    TicketTypeName = ticket.Name,
                    Quantity = item.Quantity,
                    UnitPrice = ticket.Price
                };

                totalAmount += bookingItem.TotalPrice;
                bookingItems.Add(bookingItem);
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
                    PaymentStatus = "Pending"
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

            var validatepayment = new[]
            {
                PaymentMethodConstants.Card,
                PaymentMethodConstants.JazzCash,
                PaymentMethodConstants.EasyPaisa

            };

            if (booking == null)
                throw new Exception("Booking not found");

            if (booking.UserId != userId)
                throw new UnauthorizedAccessException("Unauthorized");

            if (booking.BookingStatus != BookingStatusConstants.Pending)
                throw new Exception("Booking already processed");

            var ev = await _eventRepository.GetByIdAsync(booking.EventId);
            if (ev == null)
                throw new Exception("Event not found");

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

            //Update event
            await _eventRepository.UpdateAsync(ev);

            //Update booking status
            booking.BookingStatus = BookingStatusConstants.Confirmed;

            booking.Payment.PaymentStatus = PaymentStatusConstatants.Paid;
            booking.Payment.PaymentMethod = dto.PaymentMethod;
            booking.Payment.PaidAt = DateTime.UtcNow;

            booking.UpdatedAt = DateTime.UtcNow;

            await _bookingRepository.UpdateAsync(booking);
            await _ticketService.GenerateTicketsAsync(booking.Id, booking.UserId);

            return "Booking Confirmed";

        }


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

            //Restore tickets
            foreach (var item in booking.Items)
            {
                var ticket = ev.TicketTypes.First(t => t.Id == item.TicketTypeId);

                ticket.Sold -= item.Quantity;
            }

            await _eventRepository.UpdateAsync(ev);

            //Update booking
            booking.BookingStatus = BookingStatusConstants.Cancelled;

            booking.Payment.PaymentStatus = PaymentStatusConstatants.Refunded;

            booking.UpdatedAt = DateTime.UtcNow;

            await _bookingRepository.UpdateAsync(booking);

            return "Booking Cancelled Successfully";
        }


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



        private static BookingResponseDto MapToDto(Booking b)
        {
            return new BookingResponseDto
            {
                Id = b.Id,
                EventId = b.EventId,
                TotalAmount = b.TotalAmount,
                BookingStatus = b.BookingStatus,
                PaymentStatus = b.Payment.PaymentStatus,
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