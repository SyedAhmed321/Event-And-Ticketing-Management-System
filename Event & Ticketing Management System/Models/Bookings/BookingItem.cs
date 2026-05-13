namespace Event___Ticketing_Management_System.Models.Bookings
{
    public class BookingItem
    {
        public string TicketTypeId { get; set; } = string.Empty;
        public string TicketTypeName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
