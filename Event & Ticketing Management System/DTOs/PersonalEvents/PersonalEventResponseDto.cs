namespace Event___Ticketing_Management_System.DTOs.PersonalEvents
{
    public class PersonalEventResponseDto
    {
        public string Id { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string EventType { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public int GuestCount { get; set; }

        public string Status { get; set; } = string.Empty;

        public List<RequirementResponseDto> Requirements { get; set; } = new();

        public BudgetResponseDto Budget { get; set; } = new();

        public List<TimelineResponseDto> Timeline { get; set; } = new();
    }
}