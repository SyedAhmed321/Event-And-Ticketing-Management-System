namespace Event___Ticketing_Management_System.DTOs.PersonalEvents
{
    public class RequirementResponseDto
    {
        public string RequirementId { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsFulfilled { get; set; }

        public string? VendorId { get; set; }
    }
}