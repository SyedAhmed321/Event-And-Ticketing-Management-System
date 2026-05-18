using Event___Ticketing_Management_System.Models.Dashboard;

namespace Event___Ticketing_Management_System.DTOs.Dashboard
{
    public class DashboardResponseDto
    {
        public DashboardStats Stats { get; set; } = new();

        public RevenueAnalytics Revenue { get; set; } = new();

        public TicketAnalytics Tickets { get; set; } = new();

        public UserAnalytics Users { get; set; } = new();
    }
}