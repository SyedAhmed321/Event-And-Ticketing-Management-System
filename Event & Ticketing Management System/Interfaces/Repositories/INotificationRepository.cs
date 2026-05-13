using Event___Ticketing_Management_System.Models.Notifications;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task CreateNotificationAsync(Notification notification);
        Task<List<Notification>> GetByUserIdAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
        Task MarkAsReadAsync(string notificationId);
        Task MarkAllAsReadAsync(string userId);
        Task DeleteNotificationAsync(string notificationId);
    }
}