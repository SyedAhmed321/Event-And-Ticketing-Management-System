namespace Event___Ticketing_Management_System.DTOs.Organizers
{
    public class OrganizerRequestResponseDto
    {
        public string Id { get; set; } = string.Empty;

        public string OrganizerId { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string EventType { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public int GuestCount { get; set; }

        public decimal Budget { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}