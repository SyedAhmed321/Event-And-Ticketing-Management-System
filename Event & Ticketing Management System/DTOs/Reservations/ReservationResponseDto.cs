namespace Event___Ticketing_Management_System.DTOs.Reservations
{
    public class ReservationResponseDto
    {
        public string Id { get; set; } = string.Empty;

        public string EventId { get; set; } = string.Empty;

        public List<ReservationItemDetailsDto> Items { get; set; } = new();

        public DateTime ExpiresAt { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }

    public class ReservationItemDetailsDto
    {
        public string TicketTypeId { get; set; } = string.Empty;

        public string TicketTypeName { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}