namespace Event___Ticketing_Management_System.DTOs.Reservations
{
    public class CreateReservationDto
    {
        public string EventId { get; set; } = string.Empty;

        public List<ReservationItemDto> Items { get; set; } = new();
    }
}