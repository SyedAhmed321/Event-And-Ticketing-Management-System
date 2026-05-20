namespace Event___Ticketing_Management_System.DTOs.Admin
{
    public class SystemLogResponseDto
    {
        public string Id { get; set; } = string.Empty;

        public string Level { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string? Exception { get; set; }

        public string? Source { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}