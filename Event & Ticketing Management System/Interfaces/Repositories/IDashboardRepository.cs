using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface IDashboardRepository
    {
        //General stats
        Task<int> GetTotalUsersAsync();

        Task<int> GetTotalEventsAsync();

        Task<int> GetTotalBookingsAsync();

        Task<int> GetTotalTicketsSoldAsync();

        Task<decimal> GetTotalRevenueAsync();

        //Optional (advanced)
        Task<List<(DateTime date, decimal revenue)>> GetRevenueByDateAsync();
    }
}