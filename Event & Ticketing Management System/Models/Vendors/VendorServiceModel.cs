namespace Event___Ticketing_Management_System.Models.Vendors
{
    public class VendorServiceModel
    {
        public string ServiceId { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = string.Empty;
        // e.g. Wedding Photography

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}