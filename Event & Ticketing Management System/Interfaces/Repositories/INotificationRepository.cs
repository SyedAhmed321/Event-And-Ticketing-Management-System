using Event___Ticketing_Management_System.Models.Notifications;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task CreateAsync(Notification notification);

        Task<List<Notification>> GetByUserIdAsync(string userId);

        Task<Notification?> GetByIdAsync(string id);

        Task UpdateAsync(Notification notification);
    }
}