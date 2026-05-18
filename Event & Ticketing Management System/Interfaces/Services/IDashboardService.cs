using Event___Ticketing_Management_System.DTOs.Dashboard;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DashboardResponseDto> GetDashboardAsync();
    }
}