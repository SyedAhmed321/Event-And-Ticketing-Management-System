namespace Event___Ticketing_Management_System.DTOs.Admin
{
    public class SuspendUserDto
    {
        public string UserId { get; set; } = string.Empty;

        public bool IsSuspended { get; set; }

        public string? Reason { get; set; }
    }
}