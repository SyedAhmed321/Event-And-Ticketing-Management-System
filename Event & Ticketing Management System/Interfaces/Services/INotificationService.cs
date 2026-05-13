using static Event___Ticketing_Management_System.DTOs.Notifications.NotificationDto;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface INotificationService
    {
        Task<List<NotificationResponseDto>> GetMyNotificationsAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
        Task MarkAsReadAsync(string notificationId);
        Task MarkAllAsReadAsync(string userId);
        Task DeleteNotificationAsync(string notificationId);

        // These are called internally from other services
        Task SendBookingConfirmedAsync(string userId, string eventTitle, int ticketCount);
        Task SendBookingCancelledAsync(string userId, string eventTitle);
        Task SendTicketIssuedAsync(string userId, string eventTitle);
        Task SendEventReminderAsync(string userId, string eventTitle, DateTime eventDate);
        Task SendVendorRequestAsync(string userId, string vendorName);
        Task SendOrganizerApprovalAsync(string userId, string organizerName);
        Task SendEventCancelledAsync(string userId, string eventTitle);
    }
}
