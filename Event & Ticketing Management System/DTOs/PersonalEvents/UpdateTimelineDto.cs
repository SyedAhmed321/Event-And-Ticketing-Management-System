namespace Event___Ticketing_Management_System.DTOs.PersonalEvents
{
    public class UpdateTimelineDto
    {
        public string TimelineId { get; set; } = string.Empty;

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}