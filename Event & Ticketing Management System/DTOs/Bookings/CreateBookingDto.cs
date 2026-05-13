namespace Event___Ticketing_Management_System.DTOs.Bookings
{
    public class CreateBookingDto
    {
        public string EventId { get; set; } = string.Empty;
        public List<BookingItemDto> Items { get; set; } = new();
    }

    public class BookingItemDto
    {
        public string TicketTypeId { get; set; } = string.Empty;
        public string TicketTypeName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
