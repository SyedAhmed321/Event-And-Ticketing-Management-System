namespace Event___Ticketing_Management_System.Models.Vendors
{
    public class VendorAvailability
    {
        public List<AvailableSlot> AvailabilitySlots { get; set; } = new();
    }

    public class AvailableSlot
    {
        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string Notes { get; set; } = string.Empty;
    }
}