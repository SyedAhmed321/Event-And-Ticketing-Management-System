using Event___Ticketing_Management_System.DTOs.Tickets;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;

namespace Event___Ticketing_Management_System.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        // ── USER: Get all my tickets ─────────────────────────────────
        public async Task<List<TicketResponseDto>> GetMyTicketsAsync(string userId)
        {
            var tickets = await _ticketRepository.GetTicketsByUserIdAsync(userId);

            return tickets.Select(t => new TicketResponseDto
            {
                Id = t.Id,
                BookingId = t.BookingId,
                EventId = t.EventId,
                EventTitle = t.EventTitle,
                TicketTypeName = t.TicketTypeName,
                QRCode = t.QRCode,
                Status = t.Status,
                CheckedInAt = t.CheckedInAt,
                IssuedAt = t.IssuedAt,
                
            }).ToList();
        }

        // ── USER: Get single ticket by ID ────────────────────────────
        public async Task<TicketResponseDto> GetTicketByIdAsync(
            string ticketId, string userId)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);

            if (ticket == null)
                throw new Exception("Ticket not found");

            if (ticket.UserId != userId)
                throw new Exception("You do not have access to this ticket");

            return new TicketResponseDto
            {
                Id = ticket.Id,
                BookingId = ticket.BookingId,
                EventId = ticket.EventId,
                EventTitle = ticket.EventTitle,
                TicketTypeName = ticket.TicketTypeName,
                QRCode = ticket.QRCode,
                Status = ticket.Status,
                CheckedInAt = ticket.CheckedInAt,
                IssuedAt = ticket.IssuedAt,
                
            };
        }

        // ── USER: Get all tickets for a booking ──────────────────────
        public async Task<List<TicketResponseDto>> GetTicketsByBookingIdAsync(
            string bookingId, string userId)
        {
            var tickets = await _ticketRepository
                .GetTicketsByBookingIdAsync(bookingId);

            // Make sure these tickets belong to this user
            var userTickets = tickets.Where(t => t.UserId == userId).ToList();

            return userTickets.Select(t => new TicketResponseDto
            {
                Id = t.Id,
                BookingId = t.BookingId,
                EventId = t.EventId,
                EventTitle = t.EventTitle,
                TicketTypeName = t.TicketTypeName,
                QRCode = t.QRCode,
                Status = t.Status,
                CheckedInAt = t.CheckedInAt,
                IssuedAt = t.IssuedAt,
                
            }).ToList();
        }

        // ── ORGANIZER: Get all tickets for their event ───────────────
        public async Task<List<TicketResponseDto>> GetTicketsByEventIdAsync(string eventId)
        {
            var tickets = await _ticketRepository
                .GetTicketsByEventIdAsync(eventId);

            return tickets.Select(t => new TicketResponseDto
            {
                Id = t.Id,
                BookingId = t.BookingId,
                EventId = t.EventId,
                EventTitle = t.EventTitle,
                TicketTypeName = t.TicketTypeName,
                QRCode = t.QRCode,
                Status = t.Status,
                CheckedInAt = t.CheckedInAt,
                IssuedAt = t.IssuedAt
            }).ToList();
        }

        // ── ORGANIZER: Scan QR code at gate ─────────────────────────
        public async Task<ValidateTicketResponseDto> ValidateAndCheckInAsync(string qrCode)
        {
            var ticket = await _ticketRepository.GetTicketByQRCodeAsync(qrCode);

            // Ticket not found
            if (ticket == null)
                return new ValidateTicketResponseDto
                {
                    Valid = false,
                    Message = "Ticket not found"
                };

            // Already used
            if (ticket.CheckedInAt || ticket.Status == "Used")
                return new ValidateTicketResponseDto
                {
                    Valid = false,
                    Message = "Ticket already used",
                    TicketId = ticket.Id,
                    EventTitle = ticket.EventTitle,
                    CheckedInAt = ticket.CheckedInAt
                };

            // Cancelled
            if (ticket.Status == "Cancelled")
                return new ValidateTicketResponseDto
                {
                    Valid = false,
                    Message = "Ticket is cancelled"
                };

            // Expired
            if (ticket.Status == "Expired")
                return new ValidateTicketResponseDto
                {
                    Valid = false,
                    Message = "Ticket is expired"
                };

            // Valid — mark as checked in
            await _ticketRepository.MarkTicketCheckedInAsync(ticket.Id);

            return new ValidateTicketResponseDto
            {
                Valid = true,
                Message = "Ticket valid ✓ Entry approved",
                TicketId = ticket.Id,
                EventTitle = ticket.EventTitle,
                TicketTypeName = ticket.TicketTypeName,
                UserName = ticket.UserName,
                CheckedInAt = ticket.CheckedInAt,
            };
        }

        // ── ORGANIZER: Get attendance stats for event ────────────────
        public async Task<object> GetAttendanceAsync(string eventId)
        {
            var total = await _ticketRepository.GetTotalTicketsByEventIdAsync(eventId);
            var checkedIn = await _ticketRepository.GetCheckedInCountAsync(eventId);
            var remaining = total - checkedIn;

            return new
            {
                eventId,
                totalTickets = total,
                checkedIn,
                remaining,
                attendancePercent = total > 0
                    ? Math.Round((double)checkedIn / total * 100, 1)
                    : 0
            };
        }
    }
}