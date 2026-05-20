namespace Event___Ticketing_Management_System.DTOs.Vendors
{
    public class UpdateVendorServiceDto
    {
        public string ServiceId { get; set; } = string.Empty;

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public bool? IsAvailable { get; set; }
    }
}