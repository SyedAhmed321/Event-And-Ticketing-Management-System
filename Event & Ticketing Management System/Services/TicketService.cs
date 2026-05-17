using Event___Ticketing_Management_System.DTOs.Tickets;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Tickets;
using Event___Ticketing_Management_System.Helpers;

namespace Event___Ticketing_Management_System.Services
{
    public class TicketService : ITicketService
    {

        private readonly ITicketRepository _ticketRepository;
        private readonly IBookingRepository _bookingRepository;

        public TicketService(
            ITicketRepository ticketRepository,
            IBookingRepository bookingRepository)
        {
            _ticketRepository = ticketRepository;
            _bookingRepository = bookingRepository;
        }

        // ✅ GENERATE TICKETS AFTER CONFIRMATION
        public async Task GenerateTicketsAsync(string bookingId, string userId)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);

            if (booking == null)
                throw new Exception("Booking not found");

            var tickets = new List<Ticket>();

            foreach (var item in booking.Items)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    var ticketId = Guid.NewGuid().ToString();

                    var qrData = QRCodeHelper.GenerateQRCode(
                        ticketId,
                        booking.EventId,
                        userId
                    );

                    var ticket = new Ticket
                    {
                        Id = ticketId,
                        BookingId = booking.Id,
                        EventId = booking.EventId,
                        UserId = userId,
                        TicketTypeId = item.TicketTypeId,
                        QRCode = qrData,
                        Status = TicketStatuses.Active,
                        IssuedAt = DateTime.UtcNow,
                        CheckedIn = false
                    };

                    tickets.Add(ticket);
                }
            }

            await _ticketRepository.InsertManyAsync(tickets);
        }

        // ✅ GET USER TICKETS
        public async Task<List<TicketDto>> GetMyTicketsAsync(string userId)
        {
            var tickets = await _ticketRepository.GetByUserIdAsync(userId);

            return tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                EventId = t.EventId,
                BookingId = t.BookingId,
                TicketTypeId = t.TicketTypeId,
                Status = t.Status,
                QRCode = t.QRCode,
                CheckedIn = t.CheckedIn,
                IssuedAt = t.IssuedAt
            }).ToList();
        }

        // ✅ VALIDATE TICKET (QR SCAN)
        public async Task<TicketValidationResponseDto> ValidateTicketAsync(ValidateTicketDto dto)
        {
            var ticket = await _ticketRepository.GetByQRCodeAsync(dto.QRCode);

            if (ticket == null)
            {
                return new TicketValidationResponseDto
                {
                    IsValid = false,
                    Message = "Invalid ticket"
                };
            }

            if (ticket.Status != TicketStatuses.Active)
            {
                return new TicketValidationResponseDto
                {
                    IsValid = false,
                    Message = "Ticket is not active",
                    Status = ticket.Status
                };
            }

            if (ticket.CheckedIn)
            {
                return new TicketValidationResponseDto
                {
                    IsValid = false,
                    Message = "Ticket already used",
                    Status = TicketStatuses.Used
                };
            }

            return new TicketValidationResponseDto
            {
                IsValid = true,
                Message = "Ticket is valid",
                TicketId = ticket.Id,
                Status = ticket.Status
            };
        }

        // ✅ CHECK-IN TICKET
        public async Task<string> CheckInTicketAsync(CheckInDto dto, string staffUserId)
        {
            var ticket = await _ticketRepository.GetByQRCodeAsync(dto.QRCode);

            if (ticket == null)
                throw new Exception("Invalid ticket");

            if (ticket.CheckedIn)
                throw new Exception("Ticket already used");

            if (ticket.Status != TicketStatuses.Active)
                throw new Exception("Ticket is not valid");

            // ✅ Mark used
            ticket.CheckedIn = true;
            ticket.Status = TicketStatuses.Used;

            await _ticketRepository.UpdateAsync(ticket);

            return "Check-in successful";
        }

    }
}