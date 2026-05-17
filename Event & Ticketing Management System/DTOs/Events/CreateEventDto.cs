using Event___Ticketing_Management_System.Models.Events;
using System.ComponentModel.DataAnnotations;

namespace Event___Ticketing_Management_System.DTOs.Events
{
    public class CreateEventDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Venue { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        public string BannerImage { get; set; } = string.Empty;

        public List<string> Images { get; set; } = new();

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public List<TicketTypeDto> TicketTypes { get; set; } = new();
    }
}
