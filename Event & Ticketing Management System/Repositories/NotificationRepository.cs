using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Notifications;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMongoCollection<Notification> _notifications;

        public NotificationRepository(MongoDbService db)
        {
            _notifications = db.Database
                .GetCollection<Notification>("Notifications");
        }

        public async Task CreateNotificationAsync(Notification notification)
        {
            await _notifications.InsertOneAsync(notification);
        }

        public async Task<List<Notification>> GetByUserIdAsync(string userId)
        {
            return await _notifications
                .Find(n => n.UserId == userId)
                .SortByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            var count = await _notifications
                .CountDocumentsAsync(n => n.UserId == userId && !n.IsRead);
            return (int)count;
        }

        public async Task MarkAsReadAsync(string notificationId)
        {
            await _notifications.UpdateOneAsync(
                n => n.Id == notificationId,
                Builders<Notification>.Update.Set(n => n.IsRead, true));
        }

        public async Task MarkAllAsReadAsync(string userId)
        {
            await _notifications.UpdateManyAsync(
                n => n.UserId == userId && !n.IsRead,
                Builders<Notification>.Update.Set(n => n.IsRead, true));
        }

        public async Task DeleteNotificationAsync(string notificationId)
        {
            await _notifications.DeleteOneAsync(n => n.Id == notificationId);
        }
    }
}
