namespace Event___Ticketing_Management_System.Models.Vendors
{
    public class VendorProfile
    {
        public string BusinessName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;
        // Catering, Photography, Decoration etc.

        public string ContactEmail { get; set; } = string.Empty;

        public string ContactPhone { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public bool IsVerified { get; set; } = false;
    }
}