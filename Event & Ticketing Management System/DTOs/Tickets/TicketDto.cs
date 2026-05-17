namespace Event___Ticketing_Management_System.DTOs.Tickets
{
    // Returned to user when viewing their tickets
    public class TicketDto
    {
        public string Id { get; set; } = string.Empty;
        public string BookingId { get; set; } = string.Empty;
        public string EventId { get; set; } = string.Empty;
        public string EventTitle { get; set; } = string.Empty;
        public string TicketTypeId { get; set; } = string.Empty;
        public string QRCode { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool CheckedIn { get; set; }
        public DateTime IssuedAt { get; set; }
        
    }


    public class TicketValidationResponseDto
    {
        public bool IsValid { get; set; }

        public string Message { get; set; } = string.Empty;

        public string TicketId { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }

}