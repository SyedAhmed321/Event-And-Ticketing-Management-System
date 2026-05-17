namespace Event___Ticketing_Management_System.DTOs.Events
{
    public class EventDetailsDto
    {
        public required string Id { get; set; }
        public required string OrganizerId { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public required string Category { get; set; }

        public required string Venue { get; set; }
        public required string City { get; set; }

        public string? BannerImage { get; set; }

        public List<string> Image { get; set; } = new();

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public required string Status { get; set; }

        public required List<TicketTypeDetailsDto?> TicketTypes { get; set; }

        public int? TotalTicketsSold { get; set; }


    }
}
