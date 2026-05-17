using Event___Ticketing_Management_System.Utilities;

namespace Event___Ticketing_Management_System.DTOs.Bookings
{
    public class BookingResponseDto
    {
        public string Id { get; set; } = string.Empty;

        public string EventId { get; set; } = string.Empty;

        public List<BookingItemResponseDto> Items { get; set; } = new();

        public decimal TotalAmount { get; set; }

        public string BookingStatus { get; set; } = BookingStatusConstants.Pending;

        public string PaymentStatus { get; set; } = PaymentStatusConstatants.Pending;

        public DateTime BookingDate { get; set; }
    }

    public class BookingItemResponseDto
    {
        public string TicketTypeName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }

}
