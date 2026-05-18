using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Notifications;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMongoCollection<Notification> _notifications;

        public NotificationRepository(MongoDbService mongoDbService)
        {
            _notifications = mongoDbService.Database.GetCollection<Notification>("Notifications");
        }

        public async Task CreateAsync(Notification notification)
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

        public async Task<Notification?> GetByIdAsync(string id)
        {
            return await _notifications
                .Find(n => n.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Notification notification)
        {
            await _notifications.ReplaceOneAsync(n => n.Id == notification.Id, notification);
        }
    }
}