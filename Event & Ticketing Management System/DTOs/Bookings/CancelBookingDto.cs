namespace Event___Ticketing_Management_System.DTOs.Bookings
{
    public class CancelBookingDto
    {
        public string BookingId { get; set; } = string.Empty;
        public string CancellationReason { get; set; } = string.Empty;
    }
}
