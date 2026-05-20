namespace Event___Ticketing_Management_System.Models.Organizers
{
    public class OrganizerProfile
    {
        public string BusinessName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;
        // Wedding Planner, Corporate Events, Birthday Planner

        public string ContactEmail { get; set; } = string.Empty;

        public string ContactPhone { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public bool IsVerified { get; set; } = false;
    }
}