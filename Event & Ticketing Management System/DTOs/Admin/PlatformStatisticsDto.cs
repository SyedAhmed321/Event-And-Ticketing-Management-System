namespace Event___Ticketing_Management_System.DTOs.Admin
{
    public class PlatformStatisticsDto
    {
        public int TotalUsers { get; set; }

        public int TotalOrganizers { get; set; }

        public int TotalVendors { get; set; }

        public int TotalEvents { get; set; }

        public int TotalBookings { get; set; }

        public decimal TotalRevenue { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}