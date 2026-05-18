namespace Event___Ticketing_Management_System.DTOs.PersonalEvents
{
    public class CreatePersonalEventDto
    {
        public string Title { get; set; } = string.Empty;

        public string EventType { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public int GuestCount { get; set; }

        public decimal TotalBudget { get; set; }
    }
}