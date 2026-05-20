namespace Event___Ticketing_Management_System.Models.PersonalEvents
{
    public class EventRequirement
    {
        public string RequirementId { get; set; } = Guid.NewGuid().ToString();

        public string Category { get; set; } = string.Empty;
        // Catering, Decoration, Photography, etc.

        public string Description { get; set; } = string.Empty;

        public bool IsFulfilled { get; set; } = false;

        public string? VendorId { get; set; } // optional assigned vendor
    }
}