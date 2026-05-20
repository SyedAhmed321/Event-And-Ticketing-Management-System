namespace Event___Ticketing_Management_System.DTOs.Organizers
{
    public class HireOrganizerDto
    {
        public string OrganizerId { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public string EventType { get; set; } = string.Empty;

        public decimal Budget { get; set; }
    }
}