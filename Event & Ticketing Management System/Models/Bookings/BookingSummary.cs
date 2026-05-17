namespace Event___Ticketing_Management_System.Models.Bookings
{
    public class BookingSummary
    {
        public string BookingId { get; set; } = string.Empty;

        public string EventTitle { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public int TotalTickets { get; set; }

        public decimal TotalAmount { get; set; }

        public string BookingStatus { get; set; } = string.Empty;

        public string PaymentStatus { get; set; } = string.Empty;

    }
}
