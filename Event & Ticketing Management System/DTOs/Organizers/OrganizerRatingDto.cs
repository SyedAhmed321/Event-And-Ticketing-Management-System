namespace Event___Ticketing_Management_System.DTOs.Organizers
{
    public class OrganizerRatingDto
    {
        public string OrganizerId { get; set; } = string.Empty;

        public int Rating { get; set; } // 1–5

        public string Comment { get; set; } = string.Empty;
    }
}