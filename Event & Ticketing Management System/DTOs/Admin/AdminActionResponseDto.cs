namespace Event___Ticketing_Management_System.DTOs.Admin
{
    public class AdminActionResponseDto
    {
        public string Id { get; set; } = string.Empty;

        public string AdminId { get; set; } = string.Empty;

        public string ActionType { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? TargetId { get; set; }

        public string? TargetType { get; set; }

        public DateTime PerformedAt { get; set; }
    }
}
