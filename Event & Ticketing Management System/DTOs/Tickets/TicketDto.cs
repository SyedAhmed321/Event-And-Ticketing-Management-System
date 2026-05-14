namespace Event___Ticketing_Management_System.DTOs.Tickets
{
    // Returned to user when viewing their tickets
    public class TicketResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string BookingId { get; set; } = string.Empty;
        public string EventId { get; set; } = string.Empty;
        public string EventTitle { get; set; } = string.Empty;
        public string TicketTypeName { get; set; } = string.Empty;
        public string QRCode { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool CheckedInAt { get; set; }
        public DateTime IssuedAt { get; set; }
        
    }

    // Returned when organizer scans QR at gate
    public class ValidateTicketResponseDto
    {
        public bool Valid { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? TicketId { get; set; }
        public string? EventTitle { get; set; }
        public string? TicketTypeName { get; set; }
        public string? UserName { get; set; }
        public bool CheckedInAt { get; set; }
    }
}