namespace Event___Ticketing_Management_System.DTOs.Organizers
{
    public class CreateOrganizerDto
    {
        public string BusinessName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;

        public string ContactEmail { get; set; } = string.Empty;

        public string ContactPhone { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
    }
}