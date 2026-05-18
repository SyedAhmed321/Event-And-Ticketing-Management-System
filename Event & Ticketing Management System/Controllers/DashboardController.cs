using Event___Ticketing_Management_System.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    [Authorize(Roles = "Admin")] // ✅ usually admin-only
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // =========================================
        // ✅ GET FULL DASHBOARD DATA
        // =========================================
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _dashboardService.GetDashboardAsync();

            return Ok(new
            {
                message = "Dashboard data retrieved successfully",
                data = result
            });
        }
    }
}