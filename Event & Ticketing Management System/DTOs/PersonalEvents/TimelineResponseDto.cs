namespace Event___Ticketing_Management_System.DTOs.PersonalEvents
{
    public class TimelineResponseDto
    {
        public string TimelineId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}