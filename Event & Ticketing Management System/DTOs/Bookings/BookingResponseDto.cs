namespace Event___Ticketing_Management_System.DTOs.Bookings
{
    public class BookingResponseDto
    {
        public string BookingId { get; set; } = string.Empty;
        public string EventTitle { get; set; } = string.Empty;
        public string BookingStatus { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime BookingDate { get; set; }
        public List<TicketResponseDto> Tickets { get; set; } = new();
    }

    public class TicketResponseDto
    {
        public string TicketId { get; set; } = string.Empty;
        public string TicketTypeName { get; set; } = string.Empty;
        public string QRCode { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
