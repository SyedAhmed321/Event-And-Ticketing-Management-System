namespace Event___Ticketing_Management_System.Models.PersonalEvents
{
    public class EventTimeline
    {
        public string TimelineId { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; } = string.Empty;
        // e.g. "Cake Cutting"

        public string Description { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}