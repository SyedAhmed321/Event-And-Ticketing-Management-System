using Event___Ticketing_Management_System.DTOs.Notifications;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Notifications;

namespace Event___Ticketing_Management_System.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        // ✅ SEND NOTIFICATION
        public async Task SendAsync(string userId, string title, string message, string type)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Type = type
            };

            await _repository.CreateAsync(notification);
        }

        // ✅ GET MY NOTIFICATIONS
        public async Task<List<NotificationResponseDto>> GetMyNotificationsAsync(string userId)
        {
            var list = await _repository.GetByUserIdAsync(userId);

            return list.Select(n => new NotificationResponseDto
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            }).ToList();
        }

        // ✅ MARK AS READ
        public async Task<string> MarkAsReadAsync(string userId, string notificationId)
        {
            var notification = await _repository.GetByIdAsync(notificationId);

            if (notification == null)
                throw new Exception("Notification not found");

            if (notification.UserId != userId)
                throw new UnauthorizedAccessException("Unauthorized");

            notification.IsRead = true;

            await _repository.UpdateAsync(notification);

            return "Marked as read";
        }
    }
}
