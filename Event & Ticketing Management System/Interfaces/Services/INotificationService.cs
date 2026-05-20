using Event___Ticketing_Management_System.DTOs.Notifications;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendAsync(string userId, string title, string message, string type);

        Task<List<NotificationResponseDto>> GetMyNotificationsAsync(string userId);

        Task<string> MarkAsReadAsync(string userId, string notificationId);
    }
}
