using Event___Ticketing_Management_System.DTOs.Bookings;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Bookings;
using Event___Ticketing_Management_System.Models.Tickets;
using Event___Ticketing_Management_System.Helpers;

namespace Event___Ticketing_Management_System.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventRepository _eventRepository;
        private readonly INotificationRepository _notificationRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            ITicketRepository ticketRepository,
            IEventRepository eventRepository,
            INotificationRepository notificationRepository)
        {
            _bookingRepository = bookingRepository;
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(
            string userId, string userName, CreateBookingDto dto)
        {
            // Step 1: Get the event — FIXED: was GetEventByIdAsync, now GetByIdAsync
            var ev = await _eventRepository.GetByIdAsync(dto.EventId);
            if (ev == null)
                throw new Exception("Event not found");

            if (ev.Status != "Published")
                throw new Exception("Event is not available for booking");

            // Step 2: Check availability and deduct
            decimal totalAmount = 0;
            var bookingItems = new List<BookingItem>();

            foreach (var item in dto.Items)
            {
                var ticketType = ev.TicketTypes
                    .FirstOrDefault(t => t.Id == item.TicketTypeId);

                if (ticketType == null)
                    throw new Exception($"Ticket type {item.TicketTypeName} not found");

                int available = ticketType.Quantity - ticketType.Sold;
                if (available < item.Quantity)
                    throw new Exception($"Only {available} tickets left for {item.TicketTypeName}");

                var deducted = await _eventRepository
                    .DeductTicketQuantityAsync(dto.EventId, item.TicketTypeId, item.Quantity);

                if (!deducted)
                    throw new Exception($"Tickets sold out for {item.TicketTypeName}");

                bookingItems.Add(new BookingItem
                {
                    TicketTypeId = item.TicketTypeId,
                    TicketTypeName = item.TicketTypeName,
                    Quantity = item.Quantity,
                    UnitPrice = ticketType.Price
                });

                totalAmount += ticketType.Price * item.Quantity;
            }

            // Step 3: Create booking
            var booking = new Booking
            {
                UserId = userId,
                UserName = userName,
                EventId = dto.EventId,
                EventTitle = ev.Title,
                Items = bookingItems,
                TotalAmount = totalAmount,
                BookingStatus = "Confirmed",
                BookingDate = DateTime.UtcNow
            };

            await _bookingRepository.CreateBookingAsync(booking);

            // Step 4: Generate tickets with QR codes
            var allTickets = new List<Ticket>();

            foreach (var item in bookingItems)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    var uniqueQRContent =
                        $"{booking.Id}|{userId}|{item.TicketTypeId}|{Guid.NewGuid()}";

                    allTickets.Add(new Ticket
                    {
                        BookingId = booking.Id,
                        EventId = dto.EventId,
                        UserId = userId,
                        TicketTypeId = item.TicketTypeId,
                        TicketTypeName = item.TicketTypeName,
                        EventTitle = ev.Title,
                        QRCode = QRCodeHelper.GenerateQRCode(uniqueQRContent),
                        Status = "Active",
                        CheckedIn = false,
                        IssuedAt = DateTime.UtcNow
                    });
                }
            }

            await _ticketRepository.CreateManyTicketsAsync(allTickets);

            // Step 5: Send notification
            await _notificationRepository.CreateNotificationAsync(
                new Models.Notifications.Notification
                {
                    UserId = userId,
                    Title = "Booking Confirmed!",
                    Message = $"Your booking for {ev.Title} is confirmed. " +
                                $"{allTickets.Count} ticket(s) issued.",
                    Type = "Booking",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                });

            // Step 6: Return response
            return new BookingResponseDto
            {
                BookingId = booking.Id,
                EventTitle = ev.Title,
                BookingStatus = booking.BookingStatus,
                TotalAmount = totalAmount,
                BookingDate = booking.BookingDate,
                Tickets = allTickets.Select(t => new TicketResponseDto
                {
                    TicketId = t.Id,
                    TicketTypeName = t.TicketTypeName,
                    QRCode = t.QRCode,
                    Status = t.Status
                }).ToList()
            };
        }

        public async Task<List<BookingResponseDto>> GetMyBookingsAsync(string userId)
        {
            var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId);
            var result = new List<BookingResponseDto>();

            foreach (var booking in bookings)
            {
                var tickets = await _ticketRepository
                    .GetTicketsByBookingIdAsync(booking.Id);

                result.Add(new BookingResponseDto
                {
                    BookingId = booking.Id,
                    EventTitle = booking.EventTitle,
                    BookingStatus = booking.BookingStatus,
                    TotalAmount = booking.TotalAmount,
                    BookingDate = booking.BookingDate,
                    Tickets = tickets.Select(t => new TicketResponseDto
                    {
                        TicketId = t.Id,
                        TicketTypeName = t.TicketTypeName,
                        QRCode = t.QRCode,
                        Status = t.Status
                    }).ToList()
                });
            }

            return result;
        }

        public async Task<List<BookingResponseDto>> GetEventBookingsAsync(string eventId)
        {
            var bookings = await _bookingRepository.GetBookingsByEventIdAsync(eventId);
            var result = new List<BookingResponseDto>();

            foreach (var booking in bookings)
            {
                result.Add(new BookingResponseDto
                {
                    BookingId = booking.Id,
                    EventTitle = booking.EventTitle,
                    BookingStatus = booking.BookingStatus,
                    TotalAmount = booking.TotalAmount,
                    BookingDate = booking.BookingDate,
                    Tickets = new List<TicketResponseDto>()
                });
            }

            return result;
        }

        public async Task CancelBookingAsync(string bookingId, string userId)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);

            if (booking == null)
                throw new Exception("Booking not found");

            if (booking.UserId != userId)
                throw new Exception("You cannot cancel this booking");

            if (booking.BookingStatus == "Cancelled")
                throw new Exception("Booking is already cancelled");

            await _bookingRepository.UpdateBookingStatusAsync(bookingId, "Cancelled");

            var tickets = await _ticketRepository.GetTicketsByBookingIdAsync(bookingId);
            foreach (var ticket in tickets)
            {
                await _ticketRepository.UpdateTicketStatusAsync(ticket.Id, "Cancelled");
            }

            await _notificationRepository.CreateNotificationAsync(
                new Models.Notifications.Notification
                {
                    UserId = userId,
                    Title = "Booking Cancelled",
                    Message = $"Your booking for {booking.EventTitle} has been cancelled.",
                    Type = "Booking",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                });
        }

        public async Task<object> ValidateTicketAsync(string qrCode)
        {
            var ticket = await _ticketRepository.GetTicketByQRCodeAsync(qrCode);

            if (ticket == null)
                return new { valid = false, message = "Ticket not found" };

            if (ticket.Status == "Used" || ticket.CheckedIn)
                return new { valid = false, message = "Ticket already used" };

            if (ticket.Status == "Cancelled")
                return new { valid = false, message = "Ticket is cancelled" };

            if (ticket.Status == "Expired")
                return new { valid = false, message = "Ticket is expired" };

            await _ticketRepository.MarkTicketCheckedInAsync(ticket.Id);

            return new
            {
                valid = true,
                message = "Ticket valid ✓",
                ticketId = ticket.Id,
                eventTitle = ticket.EventTitle,
                ticketTypeName = ticket.TicketTypeName,
                userId = ticket.UserId
            };
        }
    }
}