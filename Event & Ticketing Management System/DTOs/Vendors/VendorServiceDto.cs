namespace Event___Ticketing_Management_System.DTOs.Vendors
{
    public class VendorServiceDto
    {
        public string ServiceId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
    }
}