namespace Event___Ticketing_Management_System.Models.Dashboard
{
    public class DashboardStats
    {
        public int TotalUsers { get; set; }

        public int TotalEvents { get; set; }

        public int TotalBookings { get; set; }

        public int TotalTicketsSold { get; set; }

        public decimal TotalRevenue { get; set; }

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    }
}
