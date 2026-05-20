namespace Event___Ticketing_Management_System.DTOs.Vendors
{
    public class VendorBookingDto
    {
        public required string VendorId { get; set; }

        public required string ServiceId { get; set; }

        public DateTime EventDate { get; set; }
    }
}
