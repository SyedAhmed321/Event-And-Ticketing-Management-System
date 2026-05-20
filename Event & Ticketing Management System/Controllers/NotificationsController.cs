using Event___Ticketing_Management_System.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // ✅ Helper method to extract user ID
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                   ?? throw new UnauthorizedAccessException("User not authenticated");
        }

        // =========================================
        // ✅ GET MY NOTIFICATIONS
        // =========================================
        [HttpGet]
        public async Task<IActionResult> GetMyNotifications()
        {
            var userId = GetUserId();

            var notifications = await _notificationService
                .GetMyNotificationsAsync(userId);

            return Ok(new
            {
                message = "Notifications retrieved successfully",
                data = notifications
            });
        }

        // =========================================
        // ✅ MARK NOTIFICATION AS READ
        // =========================================
        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(string id)
        {
            var userId = GetUserId();

            var result = await _notificationService
                .MarkAsReadAsync(userId, id);

            return Ok(new
            {
                message = result
            });
        }
    }
}