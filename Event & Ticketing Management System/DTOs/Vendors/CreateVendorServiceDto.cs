namespace Event___Ticketing_Management_System.DTOs.Vendors
{
    public class CreateVendorServiceDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}