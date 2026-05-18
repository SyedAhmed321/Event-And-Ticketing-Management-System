namespace Event___Ticketing_Management_System.DTOs.Organizers
{
    public class UpdateOrganizerRequestStatusDto
    {
        public string RequestId { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
        // Approved / Rejected / Completed
    }
}