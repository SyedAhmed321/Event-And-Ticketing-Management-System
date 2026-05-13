using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Users;
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

        // GET all notifications for logged in user
        [HttpGet]
        public async Task<IActionResult> GetMyNotifications()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _notificationService.GetMyNotificationsAsync(userId);
            return Ok(result);
        }

        // GET unread count — useful for showing badge on bell icon
        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var count = await _notificationService.GetUnreadCountAsync(userId);
            return Ok(new { unreadCount = count });
        }

        // PATCH mark single notification as read
        [HttpPatch("{notificationId}/read")]
        public async Task<IActionResult> MarkAsRead(string notificationId)
        {
            await _notificationService.MarkAsReadAsync(notificationId);
            return Ok(new { message = "Notification marked as read" });
        }

        // PATCH mark all notifications as read
        [HttpPatch("mark-all-read")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            await _notificationService.MarkAllAsReadAsync(userId);
            return Ok(new { message = "All notifications marked as read" });
        }

        // DELETE single notification
        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> DeleteNotification(string notificationId)
        {
            await _notificationService.DeleteNotificationAsync(notificationId);
            return Ok(new { message = "Notification deleted" });
        }
    }
}
