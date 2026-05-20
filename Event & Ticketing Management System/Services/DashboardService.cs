using Event___Ticketing_Management_System.DTOs.Dashboard;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Dashboard;

namespace Event___Ticketing_Management_System.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repository;

        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }

        //MAIN DASHBOARD METHOD
        public async Task<DashboardResponseDto> GetDashboardAsync()
        {
            //Fetch base stats
            var totalUsers = await _repository.GetTotalUsersAsync();
            var totalEvents = await _repository.GetTotalEventsAsync();
            var totalBookings = await _repository.GetTotalBookingsAsync();
            var totalTicketsSold = await _repository.GetTotalTicketsSoldAsync();
            var totalRevenue = await _repository.GetTotalRevenueAsync();

            //Build Stats object
            var stats = new DashboardStats
            {
                TotalUsers = totalUsers,
                TotalEvents = totalEvents,
                TotalBookings = totalBookings,
                TotalTicketsSold = totalTicketsSold,
                TotalRevenue = totalRevenue
            };

            //Revenue Analytics
            var revenueData = await _repository.GetRevenueByDateAsync();

            var revenueAnalytics = new RevenueAnalytics
            {
                RevenueData = revenueData.Select(r => new RevenueDataPoint
                {
                    Period = r.date.ToString("yyyy-MM-dd"),
                    Revenue = r.revenue
                }).ToList()
            };

            //Ticket Analytics (basic version)
            var ticketAnalytics = new TicketAnalytics
            {
                TicketsSold = totalTicketsSold,
                TotalTickets = totalTicketsSold, // can improve later
                TicketsAvailable = 0 // optional enhancement
            };

            //User Analytics (basic version)
            var userAnalytics = new UserAnalytics
            {
                TotalUsers = totalUsers,
                ActiveUsers = totalUsers, // placeholder
                NewUsersThisMonth = totalUsers // optional refine later
            };

            //FINAL RESPONSE
            return new DashboardResponseDto
            {
                Stats = stats,
                Revenue = revenueAnalytics,
                Tickets = ticketAnalytics,
                Users = userAnalytics
            };
        }
    }
}