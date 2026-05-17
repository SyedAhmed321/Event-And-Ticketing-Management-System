using Event___Ticketing_Management_System.Models.Events;

namespace Event___Ticketing_Management_System.DTOs.Events
{
    public class EventResponseDto
    {
        public string Id { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string BannerImage { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public List<TicketType> TicketTypes { get; set; } = new();
        public int TotalTicketsSold { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
