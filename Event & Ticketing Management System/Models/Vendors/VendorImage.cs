namespace Event___Ticketing_Management_System.Models.Vendors
{
    public class VendorImage
    {
        public string ImageId { get; set; } = Guid.NewGuid().ToString();

        public string ImageUrl { get; set; } = string.Empty;

        public string Caption { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}