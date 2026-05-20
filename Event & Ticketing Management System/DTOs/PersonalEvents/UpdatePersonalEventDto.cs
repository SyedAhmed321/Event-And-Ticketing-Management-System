namespace Event___Ticketing_Management_System.DTOs.PersonalEvents
{
    public class UpdatePersonalEventDto
    {
        public string? Title { get; set; }

        public string? Location { get; set; }

        public DateTime? EventDate { get; set; }

        public int? GuestCount { get; set; }

        public string? Status { get; set; }
    }
}