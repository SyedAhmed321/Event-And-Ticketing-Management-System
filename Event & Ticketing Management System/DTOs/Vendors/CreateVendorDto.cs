namespace Event___Ticketing_Management_System.DTOs.Vendors
{
    public class CreateVendorDto
    {
        public string BusinessName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string ContactEmail { get; set; } = string.Empty;

        public string ContactPhone { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
    }
}