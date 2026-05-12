namespace Event___Ticketing_Management_System.DTOs.Events
{
    public class CreateEventDto
    {
        public string OrganizerId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public string BannerImage { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<TicketTypeDto> TicketTypes { get; set; } = new();
    }

    public class TicketTypeDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime SalesStart { get; set; }
        public DateTime SalesEnd { get; set; }
    }
}
