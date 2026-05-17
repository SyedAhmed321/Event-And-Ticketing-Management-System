using Event___Ticketing_Management_System.Models.Events;

namespace Event___Ticketing_Management_System.DTOs.Events
{
    public class UpdateEventDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

        public string? Category { get; set; }
        public string? Venue { get; set; }
        public string? City { get; set; }
        public string? BannerImage { get; set; }
        public List<string>? Image { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? Status { get; set; }
    }
}
